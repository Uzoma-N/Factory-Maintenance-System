using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace Maintenance_system
{
    public partial class User_Interface : Form
    {
        public static Graph Sys_graph = new Graph();
        public static Graph Sys_rule = new Graph();
        public static Notation3Parser passe = new Notation3Parser();
        Inconsistency_Check InconsistencyObject = new Inconsistency_Check();
       
        public User_Interface()
        {
            InitializeComponent();

        }

        class KpiObject
        {
            public string KPIusage;

            public string Key;
            public string Value;
            public string Min;
            public string Max;
            public string Unit;
            public KpiObject(string KPIusage)
            {
                this.KPIusage = KPIusage;
            }

        }
        public static Dictionary<string, List<double>> csv = new Dictionary<string, List<double>>();
               
        double[] Input = new double[3];
        double[,] NNMat = new double[3, 6];
        TcpListener server;
        int Port_Number = 1337;
        

        private void Form1_Load(object sender, EventArgs e)
        {            
            passe.Load(Sys_graph, @"Knowledge_Base.n3");
            passe.Load(Sys_rule, @"rules.n3");
            //server = new TcpListener(IPAddress.Parse(GetIP()), Port_Number);
            //ServerStatus.Text = "Server inactive...";
            applyANN.Enabled = false;

            string query_1 = @"
                    
                prefix ind:<URN:inds:>                
                prefix classes:<URN:class:>
                prefix rdfs:<http://www.w3.org/2000/01/rdf-schema#>
                    SELECT ?pr ?label
                       {
	                        ?pr	a classes:MProcess;
								rdfs:label ?label.
                                          }";

            SparqlResultSet Nice1 = (SparqlResultSet)Sys_graph.ExecuteQuery(query_1);

            String[] Reply = new string[0];
            List<string> Replylist = Reply.ToList();

            foreach (SparqlResult srs in Nice1)
            {
                INode ReplyId = srs[0];
                INode ReplyLabel = srs[1];
                Replylist.Add(ReplyId.ToString());
                Replylist.Add(ReplyLabel.ToString());
            }

            Reply = Replylist.ToArray();
            Expandtreeview(treeView, Reply);
        }

        private void Expandtreeview(TreeView treeView, string[] Rule)
        {
            try
            {
                treeView.Nodes.Clear(); // Clear all            
                TreeNode rootNode = new TreeNode
                {
                    Tag = Rule[0],
                    Text = Rule[1] // Node label display content
                };

                treeView.Nodes.Add(rootNode); // Add root directory

                string[] subLabels = new string[0];
                string[] subNodes = new string[0];
                List<string> subLabelsList = subLabels.ToList();
                List<string> subNodesList = subNodes.ToList();

                // for subnodes
                QueryNodes(Rule[0], subLabelsList, subNodesList);

                subLabels = subLabelsList.ToArray();
                subNodes = subNodesList.ToArray();

                for (int i = 0; i < subLabels.Length; i++)
                {
                    TreeNode currentNode = rootNode.Nodes.Add(subLabels[i]);
                    currentNode.Tag = subNodes[i];
                    AssignNode(rootNode.Nodes[i], subNodes[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\nError location");
            }
        }

        private bool AssignNode(TreeNode treeNode, string nodeUrn)
        {
            string[] subLabels = new string[0];
            string[] subUrns = new string[0];
            List<string> subLabelsList = subLabels.ToList();
            List<string> subNodesList = subUrns.ToList();

            QueryNodes(nodeUrn, subLabelsList, subNodesList);

            subLabels = subLabelsList.ToArray();
            subUrns = subNodesList.ToArray();
            if (subLabels.Length == 1 && subLabels[0].Equals(""))
            {
                return false;
            }
            for (int i = 0; i < subLabels.Length; i++)
            {
                TreeNode currentNode = treeNode.Nodes.Add(subLabels[i]);
                currentNode.Tag = subUrns[i];
                AssignNode(treeNode.Nodes[i], subUrns[i]);
            }
            return true;
        }

        private void QueryNodes(string nodeUrn, List<string> subLabelsList, List<string> subNodesList)
        {
            string query_2 = "Prefix ind:<URN:inds:>" +
                "prefix classes:<URN:class:>" +
                "prefix prop:<URN:props:>" +
                "prefix rdfs:<http://www.w3.org/2000/01/rdf-schema#>" +
                "SELECT ?label ?subprocess " +
                "WHERE{ " +
                "<" + nodeUrn + "> prop:hasSubProcess ?subprocess." +
                "?subprocess rdfs:label ?label . }";

            SparqlResultSet Nice2 = (SparqlResultSet)Sys_graph.ExecuteQuery(query_2);

            if (Nice2 is SparqlResultSet reply)
            {
                foreach (SparqlResult result in reply)
                {
                    INode SubNodeReplyLabel = result["label"];
                    INode SubNodeReplySubprocess = result["subprocess"];
                    subLabelsList.Add(SubNodeReplyLabel.ToString());
                    subNodesList.Add(SubNodeReplySubprocess.ToString());
                }
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            nodeDetails.Text = "";
            string selectedNode = treeView.SelectedNode.Tag.ToString();
            string[] query_3 = new string[]{"Prefix ind:<URN:inds:>" +
                "prefix classes:<URN:class:>" +
                "prefix prop:<URN:props:>" +
                "prefix rdfs:<http://www.w3.org/2000/01/rdf-schema#>" +
                "SELECT ?Kpi ?Kpilabel ?KpiInfo WHERE" +
                "{<"+selectedNode+"> prop:hasKPI ?Kpi ." +
                " ?Kpi rdfs:label ?Kpilabel ;" +
                "         prop:hasComment ?KpiInfo .}",
                "SELECT ?Inputlabel ?InputComment WHERE" +
                "{<"+selectedNode+"> prop:hasInput ?Input ." +
                " ?Input rdfs:label ?Inputlabel ;" +
                "           prop:hasComment ?InputComment .}",
                "SELECT ?Outputlabel ?OutputComment WHERE" +
                "{<"+selectedNode+"> prop:hasOutput ?Output ." +
                " ?Output rdfs:label ?Outputlabel ;" +
                "            prop:hasComment ?OutputComment .}",
                "SELECT  ?KpiMin ?KpiMax ?KpiUsage ?KpiUnit WHERE" +
                "{<" + selectedNode + "> prop:hasKPI ?Kpi ." +
                " ?Kpi prop:hasInter ?inter ." +
                "  ?inter prop:hasMin ?KpiMin;" +
                " prop:hasMax ?KpiMax;" +
                " prop:hasUsage ?KpiUsage;" +
                " prop:hasUnit ?KpiUnit .}"
            };

            List<KpiObject> kpiObjectArray = new List<KpiObject>();

            Dictionary<string, string> kpiDetails = new Dictionary<string, string>();
            Dictionary<string, string> inputDetails = new Dictionary<string, string>();
            Dictionary<string, string> outputDetails = new Dictionary<string, string>();

            String[] KpiDetail = new string[0];
            List<string> KpiDetaillist = KpiDetail.ToList();

            SparqlQueryParser parser = new SparqlQueryParser();
            int i = 0;
            SparqlParameterizedString queryAll = new SparqlParameterizedString();
            KpiObject newkpi = null;
            foreach (string querySentence in query_3)
            {
                queryAll.CommandText = querySentence;
                SparqlQuery multiplequery = parser.ParseFromString(queryAll);
                SparqlResultSet Nice3 = (SparqlResultSet)Sys_graph.ExecuteQuery(multiplequery);

                if (Nice3 is SparqlResultSet)
                {
                    SparqlResultSet nice3 = (SparqlResultSet)Nice3;

                    foreach (SparqlResult result in nice3)
                    {
                        if (result.HasValue("Kpilabel"))
                        {
                            INode KPIlabel = result["Kpilabel"];
                            INode KPIinfo = result["KpiInfo"];
                            INode KPIusage = result["Kpi"];
                            newkpi = new KpiObject(KPIusage.ToString());

                            kpiDetails.Add(KPIlabel.ToString(), KPIinfo.ToString());
                            newkpi.Key = KPIlabel.ToString();
                            newkpi.Value = KPIinfo.ToString();

                            kpiObjectArray.Add(newkpi);
                        }

                        if (result.HasValue("Inputlabel"))
                        {
                            INode Inputlabel = result["Inputlabel"];
                            INode Inputcomment = result["InputComment"];
                            inputDetails.Add(Inputlabel.ToString(), Inputcomment.ToString());
                        }

                        if (result.HasValue("Outputlabel"))
                        {
                            INode Outputlabel = result["Outputlabel"];
                            INode Outputcomment = result["OutputComment"];
                            outputDetails.Add(Outputlabel.ToString(), Outputcomment.ToString());
                        }

                        if (result.HasValue("KpiMin"))
                        {
                            INode KPIMin = result["KpiMin"];
                            INode KPIusage = result["KpiUsage"];

                            if (KPIMin != null)
                            {
                                KpiDetaillist.Add(KPIMin.ToString());
                                foreach (KpiObject item in kpiObjectArray)
                                {
                                    if (item.KPIusage.CompareTo(KPIusage.ToString()) == 0)
                                    {
                                        item.Min = KPIMin.ToString();
                                        break;
                                    }
                                }
                            }
                        }

                        if (result.HasValue("KpiMax"))
                        {
                            INode KPIMax = result["KpiMax"];
                            INode KPIusage = result["KpiUsage"];
                            if (KPIMax != null)
                            {
                                KpiDetaillist.Add(KPIMax.ToString());

                                foreach (KpiObject item in kpiObjectArray)
                                {
                                    if (item.KPIusage.CompareTo(KPIusage.ToString()) == 0)
                                    {
                                        item.Max = KPIMax.ToString();
                                        break;
                                    }
                                }
                            }
                        }

                        if (result.HasValue("KpiUnit"))
                        {
                            INode KPIUnit = result["KpiUnit"];
                            INode KPIusage = result["KpiUsage"];
                            if (KPIUnit != null)
                            {
                                KpiDetaillist.Add(KPIUnit.ToString());

                                foreach (KpiObject item in kpiObjectArray)
                                {
                                    if (item.KPIusage.CompareTo(KPIusage.ToString()) == 0)
                                    {
                                        item.Unit = KPIUnit.ToString();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                i++;
            }

            nodeDetails.Text += "\t" + treeView.SelectedNode.Text + " " + "Node Information" + "\r\n";
            for (int j = 0; j < 3; j++)
            {
                if (j == 0)
                {
                    nodeDetails.Text += "About KPI:\r\n";

                    int k = 1;
                    foreach (var item in kpiObjectArray)
                    {
                        nodeDetails.Text += k + ".";
                        nodeDetails.Text += "\t" + item.Key + "\r\n";
                        nodeDetails.Text += "\t" + item.Value + "\r\n";
                        if (item.Unit != null)
                        {
                            nodeDetails.Text += "\tIt is measured in " + item.Unit + "\r\n";
                        }
                        if (item.Min != null)
                        {
                            nodeDetails.Text += "\tMin Value:" + item.Min + "\r\n";
                        }
                        if (item.Max != null)
                        {
                            nodeDetails.Text += "\tMax Value:" + item.Max + "\r\n";
                        }
                        nodeDetails.Text += "\r\n";
                        k++;
                    }
                }
                else if (j == 1)
                {
                    nodeDetails.Text += "About Input:\r\n";
                    int k = 1;
                    foreach (var item in inputDetails)
                    {
                        nodeDetails.Text += k + ".";
                        nodeDetails.Text += "\t" + item.Key + "\r\n";
                        nodeDetails.Text += "\t" + item.Value + "\r\n\r\n";
                        k++;
                    }
                }
                else if (j == 2)
                {
                    nodeDetails.Text += "About Output:\r\n";
                    int k = 1;
                    foreach (var item in outputDetails)
                    {
                        nodeDetails.Text += k + ".";
                        nodeDetails.Text += "\t" + item.Key + "\r\n";
                        nodeDetails.Text += "\t" + item.Value + "\r\n\r\n";
                        k++;
                    }
                }

            }
        }

        private void nodeDetails_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnInconCheck_Click(object sender, EventArgs e)
        {

            formInconsistency Check = new formInconsistency();
            Check.ShowDialog();
        }

        private void useFormular_Click(object sender, EventArgs e)
        {

            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();

            using (var rd = new StreamReader("KBE.csv"))
            {
                var splits = rd.ReadLine().Split(',');
                csv.Add(splits[0].Trim(), new List<double>());
                csv.Add(splits[1].Trim(), new List<double>());
                csv.Add(splits[2].Trim(), new List<double>());
                csv.Add(splits[3].Trim(), new List<double>());
                csv.Add(splits[4].Trim(), new List<double>());
                csv.Add(splits[5].Trim(), new List<double>());
                while (!rd.EndOfStream)
                {
                    splits = rd.ReadLine().Split(',');
                    csv["Daily Production"].Add(Double.Parse(splits[0]));
                    csv["Speed"].Add(Double.Parse(splits[1]));
                    csv["Pressure"].Add(Double.Parse(splits[2]));
                    csv["Sliding Distance"].Add(Double.Parse(splits[3]));
                    csv["Total Production"].Add(Double.Parse(splits[4]));
                    csv["Bad Production"].Add(Double.Parse(splits[5]));
                }
            }

            chartValues.Series.Clear();
            var chart = chartValues.ChartAreas[0];
            
            chart.AxisX.LabelStyle.IsEndLabelVisible = true;
            chart.AxisX.Minimum = 0;
            chart.AxisY.Minimum = 0;
            chart.AxisY.Maximum = 100000;
                        
            chartValues.Series.Add("Daily Production");
            chartValues.Series["Daily Production"].ChartType = SeriesChartType.Line;
            chartValues.Series["Daily Production"].Color = Color.Red;
            chartValues.Series["Daily Production"].Points.DataBindY(csv["Daily Production"]);
                        
            chartValues.Series.Add("Speed");
            chartValues.Series["Speed"].ChartType = SeriesChartType.Line;
            chartValues.Series["Speed"].Color = Color.Blue;
            chartValues.Series["Speed"].Points.DataBindY(csv["Speed"]);

            chartValues.Series.Add("Pressure");
            chartValues.Series["Pressure"].ChartType = SeriesChartType.Line;
            chartValues.Series["Pressure"].Color = Color.Black;
            chartValues.Series["Pressure"].Points.DataBindY(csv["Pressure"]);

            chartValues.Series.Add("Sliding Distance");
            chartValues.Series["Sliding Distance"].ChartType = SeriesChartType.Line;
            chartValues.Series["Sliding Distance"].Color = Color.Brown;
            chartValues.Series["Sliding Distance"].Points.DataBindY(csv["Sliding Distance"]);

            chartValues.Series.Add("Total Production");
            chartValues.Series["Total Production"].ChartType = SeriesChartType.Line;
            chartValues.Series["Total Production"].Color = Color.Green;
            chartValues.Series["Total Production"].Points.DataBindY(csv["Total Production"]);

            chartValues.Series.Add("Bad Production");
            chartValues.Series["Bad Production"].ChartType = SeriesChartType.Line;
            chartValues.Series["Bad Production"].Color = Color.Chocolate;
            chartValues.Series["Bad Production"].Points.DataBindY(csv["Bad Production"]); 

             //for Efficiency
             string[] input1 = new string[0];
            string[] output1 = new string[0];
            string[] file1 = new string[0];
            List<string> Inputs1 = input1.ToList();
            List<string> Outputs1 = output1.ToList();
            List<string> File1 = file1.ToList();

            string nodestate = "URN:inds:Efficiency";
            GetKPIinfo(nodestate, Inputs1, Outputs1, File1);

            input1 = Inputs1.ToArray();
            output1 = Outputs1.ToArray();
            file1 = File1.ToArray();

            engine.ExecuteFile(@file1[0], scope);
            dynamic Efficiency = scope.GetVariable("efficiency1");
            Input[1] = Efficiency(csv["Speed"], csv["Daily Production"]);
            
            textEff.Text = Input[1].ToString();

            //for wear rate
            string[] input2 = new string[0];
            string[] output2 = new string[0];
            string[] file2 = new string[0];
            List<string> Inputs2 = input2.ToList();
            List<string> Outputs2 = output2.ToList();
            List<string> File2 = file2.ToList();

            string nodestate2 = "URN:inds:WearRate";
            GetKPIinfo(nodestate2, Inputs2, Outputs2, File2);

            input2 = Inputs2.ToArray();
            output2 = Outputs2.ToArray();
            file2 = File2.ToArray();

            engine.ExecuteFile(@file2[0], scope);
            dynamic WeRate = scope.GetVariable("WearRate");
            Input[0] = WeRate(csv["Pressure"], csv["Sliding Distance"]);
            textWear.Text = Input[0].ToString();

            //for quality tracking
            string[] input3 = new string[0];
            string[] output3 = new string[0];
            string[] file3 = new string[0];
            List<string> Inputs3 = input3.ToList();
            List<string> Outputs3 = output3.ToList();
            List<string> File3 = file3.ToList();

            string nodestate3 = "URN:inds:QualityTracking";
            GetKPIinfo(nodestate3, Inputs3, Outputs3, File3);

            input3 = Inputs3.ToArray();
            output3 = Outputs3.ToArray();
            file3 = File3.ToArray();

            engine.ExecuteFile(@file3[0], scope);
            dynamic QTrack = scope.GetVariable("QualityTrack");
            Input[2] = QTrack(csv["Bad Production"], csv["Total Production"]);
            textQuaT.Text = Input[2].ToString();

            applyANN.Enabled = true;
        }

        string GetStateQuery(string State) //this function calls the query
        {
            string query_1 =
                            "prefix ind:<URN:inds:>" +
                            "prefix prop:<URN:props:>" +
                            "prefix classes:<URN:class:>" +
                            "prefix rdfs:<http://www.w3.org/2000/01/rdf-schema#>" +
                    "select ?Inlabel ?label ?File " +
                        "where {" +
                             "<" + State + "> prop:hasFormular ?formular;" +
                             " rdfs:label ?label ." +
                             "?formular prop:hasInvar ?Invar;" +
                             " prop:usesFile ?File ." +
                             " ?Invar rdfs:label ?Inlabel.}";
            return query_1;
        }

        private void GetKPIinfo(string KPI, List<string> InvarLabel, List<string> OutvarLabel, List<string> FormFile)
        {
            string query = GetStateQuery(KPI);
            SparqlResultSet Nice = (SparqlResultSet)Sys_graph.ExecuteQuery(query);

            foreach (SparqlResult result in Nice)
            {
                INode Invar = result["Inlabel"];
                INode Outvar = result["label"];
                INode File = result["File"];
                InvarLabel.Add(Invar.ToString());
                OutvarLabel.Add(Outvar.ToString());
                FormFile.Add(File.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            predictBox.Text = "";
            textQuaT.Text = "";
            textEff.Text = "";
            textWear.Text = "";
            minBox.Text = "";
            maxBox.Text = "";            
            chartValues.Series.Clear();
            csv.Remove("Daily Production");
            csv.Remove("Speed");
            csv.Remove("Pressure");
            csv.Remove("Sliding Distance");
            csv.Remove("Total Production");
            csv.Remove("Bad Production");
        }

        private void applyANN_Click(object sender, EventArgs e)
        {

            string[] UsageColl1 = new string[0];
            string[] MinColl1 = new string[0];
            string[] MaxColl1 = new string[0];
            List<string> UsageVal1 = UsageColl1.ToList();
            List<string> MinVal1 = MinColl1.ToList();
            List<string> MaxVal1 = MaxColl1.ToList();

            string State1 = "URN:inds:Critical3";

            getNodeValues(State1, UsageVal1, MinVal1, MaxVal1);

            UsageColl1 = UsageVal1.ToArray();
            MinColl1 = MinVal1.ToArray();
            MaxColl1 = MaxVal1.ToArray();

            //for second state
            string[] UsageColl2 = new string[0];
            string[] MinColl2 = new string[0];
            string[] MaxColl2 = new string[0];
            List<string> UsageVal2 = UsageColl2.ToList();
            List<string> MinVal2 = MinColl2.ToList();
            List<string> MaxVal2 = MaxColl2.ToList();

            string State2 = "URN:inds:Critical2";

            getNodeValues(State2, UsageVal2, MinVal2, MaxVal2);

            UsageColl2 = UsageVal2.ToArray();
            MinColl2 = MinVal2.ToArray();
            MaxColl2 = MaxVal2.ToArray();

            //for third state
            string[] UsageColl3 = new string[0];
            string[] MinColl3 = new string[0];
            string[] MaxColl3 = new string[0];
            List<string> UsageVal3 = UsageColl3.ToList();
            List<string> MinVal3 = MinColl3.ToList();
            List<string> MaxVal3 = MaxColl3.ToList();

            string State3 = "URN:inds:Critical1";

            getNodeValues(State3, UsageVal3, MinVal3, MaxVal3);

            UsageColl3 = UsageVal3.ToArray();
            MinColl3 = MinVal3.ToArray();
            MaxColl3 = MaxVal3.ToArray();


            double[] WeightMin1 = new double[3];
            double[] WeightMax1 = new double[3];
            getWeightValues(MinColl1, MaxColl1, WeightMin1, WeightMax1);

            double[] WeightMin2 = new double[3];
            double[] WeightMax2 = new double[3];
            getWeightValues(MinColl2, MaxColl2, WeightMin2, WeightMax2);

            double[] WeightMin3 = new double[3];
            double[] WeightMax3 = new double[3];
            getWeightValues(MinColl3, MaxColl3, WeightMin3, WeightMax3);

            for (int i = 0; i < WeightMin1.Length; i++)
            {
                NNMat[i, 0] = WeightMin1[i];
                NNMat[i, 1] = WeightMax1[i];
                NNMat[i, 2] = WeightMin2[i];
                NNMat[i, 3] = WeightMax2[i];
                NNMat[i, 4] = WeightMin3[i];
                NNMat[i, 5] = WeightMax3[i];
            }
            double[,] FLNeurons = new double[3, 6];//matrix of first-layer neurons
            double[,] SLNeurons = new double[1, 9];//matrix for the second Layer neurons
            double[] OLNeurons = new double[3];//matrix of output-layer neurons

            getFLNeurons(NNMat, Input, FLNeurons);

            getSLNeurons(FLNeurons, SLNeurons);
            if (SLNeurons[0, 2] == 1)
            {
                predictBox.Text = "\rThe wear rate of this asset is high. Please, check the lubricant state and other possible causes.\r\n";
            }
            if (SLNeurons[0, 5] == 1)
            {
                predictBox.Text += "The efficiency of this asset is low. Please, ensure all standsrd processes are followed. Report your finding.\r\n";
            }
            if (SLNeurons[0, 8] == 1)
            {
                predictBox.Text += "The quality of product from this asset is low. Please, inform the quality department.\r\n";
            }
            getOLNeurons(SLNeurons, OLNeurons);



            if (OLNeurons[2] == 0.5)
            {
                predictBox.Text += "Please, follow instructions above.\r\n";

                if (OLNeurons[0] == OLNeurons[1])
                {
                    minBox.Text = 6.ToString();
                    maxBox.Text = 7.ToString();
                }
                else if (OLNeurons[0] < OLNeurons[1])
                {
                    minBox.Text = 5.ToString();
                    maxBox.Text = 6.ToString();
                }
                else if (OLNeurons[0] > OLNeurons[1])
                {
                    minBox.Text = 7.ToString();
                    maxBox.Text = 8.ToString();
                }
            }
            else if (OLNeurons[2] == 1.5)
            {
                predictBox.Text += "Please, follow instructions above.\r\n";

                if (OLNeurons[0] < OLNeurons[1])
                {
                    minBox.Text = 3.ToString();
                    maxBox.Text = 4.ToString();
                }
                else if (OLNeurons[0] > OLNeurons[1])
                {
                    minBox.Text = 5.ToString();
                    maxBox.Text = 6.ToString();
                }
            }
            else if (OLNeurons[2] == 2.5)
            {
                predictBox.Text += "Asset Condition is very bad.\r\n";
                minBox.Text = 2.ToString();
                maxBox.Text = 3.ToString();
            }
            else if (OLNeurons[2] < 0)

                if (OLNeurons[1] == 1.5)
                {
                    predictBox.Text += "\r\nAsset Condition is average.\r\nPerform your daily checks.\r\n";
                    predictBox.Text += ".";
                    minBox.Text = 7.ToString();
                    maxBox.Text = 8.ToString();
                }
                else if (OLNeurons[0] == 1.5)
                {
                    predictBox.Text += "\r\nAsset Condition is good.\r\nPerform your daily checks.\r\n";
                    minBox.Text = 9.ToString();
                    maxBox.Text = 10.ToString();
                }
                else if (OLNeurons[1] ==2.5)
                {
                    predictBox.Text += "\r\nAsset Condition is average.\r\nPerform your daily checks.\r\n";
                    minBox.Text = 6.ToString();
                    maxBox.Text = 7.ToString();
                }
                else if (OLNeurons[2] ==2.5)
                {
                    predictBox.Text += "\r\nAsset Condition is good.\r\nPerform your daily checks.\r\n";
                    minBox.Text = 10.ToString();
                    maxBox.Text = 12.ToString();
                }

            applyANN.Enabled = false;
        }

        string GetStatQuery(string State) //this function calls the query
        {
            string query_1 =
                            "prefix ind:<URN:inds:>" +
                            "prefix prop:<URN:props:>" +
                            "prefix classes:<URN:class:>" +
                            "prefix rdfs:<http://www.w3.org/2000/01/rdf-schema#>" +
                    "select ?usage ?min ?max " +
                        "where {" +
                             "<" + State + "> prop:isCaused ?kpiInter ." +
                             "?kpiInter prop:hasUsage ?usage;" +
                             " prop:hasMin ?min;" +
                             " prop:hasMax ?max .}";
            return query_1;
        }

        private void getNodeValues(string Nodestate, List<string> UsageVal, List<string> MinVal, List<string> MaxVal)
        {
            string query = GetStatQuery(Nodestate);

            SparqlResultSet Nice1 = (SparqlResultSet)Sys_graph.ExecuteQuery(query);

            if (Nice1 is SparqlResultSet reply)
            {
                foreach (SparqlResult result in reply)
                {
                    INode Usage = result["usage"];
                    INode Min = result["min"];
                    INode Max = result["max"];
                    UsageVal.Add(Usage.ToString());
                    MinVal.Add(Min.ToString());
                    MaxVal.Add(Max.ToString());
                }
            }
        }

        private void getWeightValues(string[] MinVal, string[] MaxVal, double[] Weightmin, double[] Weightmax)
        {
            for (int j = 0; j < MinVal.Length; j++)
            {
                double[] invW = new double[3];
                invW[j] = Convert.ToDouble(MinVal[j]);
                Weightmin[j] = 1 / (double)invW[j]; //derive the inverse of the limit value       
            }

            for (int j = 0; j < MaxVal.Length; j++)
            {
                double[] invW = new double[3];
                invW[j] = Convert.ToDouble(MaxVal[j]);
                Weightmax[j] = -1 / (double)invW[j]; //derive the inverse of the limit value              
            }
        }

        private void getFLNeurons(double[,] Mat, double[] input, double[,] neuron)
        {
            int minBias = -1;  //for the first layer neuron computation
            int maxBias = 1;

            for (int i = 0; i < Mat.GetLength(0); i++)     //Mat[3,6]
            {
                for (int j = 0; j < Mat.GetLength(1); j += 2)
                {
                    neuron[i, j] = Mat[i, j] * input[i] + minBias; //deriving first neuron
                    //using binary threshold method 
                    if (neuron[i, j] >= 0)
                    {
                        neuron[i, j] = 1;
                    }
                    else
                        neuron[i, j] = 0;
                }
                for (int j = 1; j < Mat.GetLength(1); j += 2)
                {
                    neuron[i, j] = Mat[i, j] * input[i] + maxBias; //deriving first neuron
                    //using binary threshold method 
                    if (neuron[i, j] >= 0)
                    {
                        neuron[i, j] = 1;
                    }
                    else
                        neuron[i, j] = 0;
                }
            }
        }

        private void getSLNeurons(double[,] Neuron, double[,] Neuron2)
        {
            int k = 0;
            int t = 0;
            double[,] resetting = new double[2, 9];
            double[,] Mult = new double[1, 9];
            int[,] weight2 = new int[1, 2]
            {
                {1, 1}
            };


            double Bias2 = -1.5;
            for (int i = 0; i < Neuron.GetLength(0); i++)     //Neuron[3,6]
            {
                for (int j = 0; j < Neuron.GetLength(1); j += 2)
                {
                    resetting[0, k] = Neuron[i, j];
                    k++;
                }
                for (int j = 1; j < Neuron.GetLength(1); j += 2)
                {
                    resetting[1, t] = Neuron[i, j];
                    t++;
                }
            }
            //using [w][i]+[b]
            for (int i = 0; i < Mult.GetLength(0); i++)
            {
                for (int j = 0; j < Mult.GetLength(1); j++)
                {
                    Mult[i, j] = 0;
                    for (int p = 0; p < weight2.GetLength(1); p++)
                    {
                        Mult[i, j] += weight2[i, p] * resetting[p, j]; //Matrix Multiplication
                    }
                }
            }
            for (int j = 0; j < Mult.GetLength(1); j++)
            {
                Neuron2[0, j] = Mult[0, j] + Bias2;
                //using binary threshold method
                if (Neuron2[0, j] >= 0)
                {
                    Neuron2[0, j] = 1;
                }
                else
                    Neuron2[0, j] = 0;
            }
        }

        private void getOLNeurons(double[,] Neuron, double[] Neuron2)
        {
            int k = 0;
            int t = 0;
            int y = 0;
            double[,] reset2 = new double[3, 3];
            double[,] mult2 = new double[1, 3];
            int[,] weight3 = new int[1, 3]
            {
                {1,1,1}
            };
            double Bias3 = -0.5;

            for (int i = 0; i < Neuron.GetLength(0); i++)
            {
                //convert from a 1x9 matrix to a 3x3 matrix
                for (int j = 0; j < Neuron.GetLength(1); j += 3)      //Neuron [1,9]
                {
                    reset2[k, 0] = Neuron[i, j];
                    k++;
                }
                for (int j = 1; j < Neuron.GetLength(1); j += 3)
                {
                    reset2[t, 1] = Neuron[i, j];
                    t++;
                }
                for (int j = 2; j < Neuron.GetLength(1); j += 3)
                {
                    reset2[y, 2] = Neuron[i, j];
                    y++;
                }
            }
            //using [w][i]+[b]
            for (int i = 0; i < mult2.GetLength(0); i++)             //Neuron2[1,3]
            {
                for (int j = 0; j < Neuron2.Length; j++)
                {
                    mult2[i, j] = 0;
                    for (int p = 0; p < weight3.GetLength(1); p++)
                    {
                        mult2[i, j] += weight3[i, p] * reset2[p, j]; //Matrix Multiplication
                    }
                }
            }
            for (int j = 0; j < Neuron2.Length; j++)
            {
                Neuron2[j] = mult2[0, j] + Bias3;
            }
        }    
       
        private void btnStart_Click_1(object sender, EventArgs e)
        {
            formServer Connect = new formServer();
            Connect.ShowDialog();
        }

        private void chartValues_Click(object sender, EventArgs e)
        {

        }
    }
}
