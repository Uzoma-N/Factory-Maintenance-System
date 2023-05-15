using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;

namespace Maintenance_system
{
    class Inconsistency_Check
    {

        public string Checks()
        {
            
            //DomainRule
            string[] domainVar1 = new string[0];
            string[] domainVar2 = new string[0];
            List<string> Propertylist1 = domainVar1.ToList();
            List<string> ObjectList1 = domainVar2.ToList();

            string State1 = "http://www.w3.org/2000/01/rdf-schema#domain";

            GetRule1(State1, Propertylist1, ObjectList1);

            domainVar1 = Propertylist1.ToArray();
            domainVar2 = ObjectList1.ToArray();
            string value = "";
            value += "Result of Inconsistency Check for Domain Rule";

                
           // icheckDetail.Text += "Result of Inconsistency Check for Domain Rule";
            for (int i = 0; i < domainVar1.Length; i++)
            {
                string Check = domainRule(domainVar1[i]);
                SparqlResultSet Nice = (SparqlResultSet)User_Interface.Sys_graph.ExecuteQuery(Check);

                foreach (SparqlResult result in Nice)
                {

                    if (result["value"].ToString() == domainVar2[i].ToString())
                    {
                        value+="";
                       // icheckDetail.Text += "";
                    }
                    else
                    {
                        value += "\r\n!ERROR!\r\n";
                        value+= result["sub"].ToString() + " in " + result["value"].ToString() + " is an error!\r\n";
                    }
                }
            }

            //RangeRule
            string[] rangeVar1 = new string[0];
            string[] rangeVar2 = new string[0];
            List<string> Propertylist2 = rangeVar1.ToList();
            List<string> ObjectList2 = rangeVar2.ToList();

            string State2 = "http://www.w3.org/2000/01/rdf-schema#range";

            GetRule1(State2, Propertylist2, ObjectList2);

            rangeVar1 = Propertylist2.ToArray();
            rangeVar2 = ObjectList2.ToArray();

            value+= "\r\nResult of Inconsistency Check for Range Rule";
            for (int i = 0; i < rangeVar1.Length; i++)
            {
                string Check = rangeRule(rangeVar1[i]);
                SparqlResultSet Nice1 = (SparqlResultSet)User_Interface.Sys_graph.ExecuteQuery(Check);

                foreach (SparqlResult result in Nice1)
                {
                    if (result["value"].ToString() == rangeVar2[i].ToString())
                    {
                        value+= "";
                    }
                    else
                    {
                        value+= "\r\n!ERROR!\r\n";
                        value+= result["object"].ToString() + " in " + result["value"].ToString() + " is an error!\r\n";
                    }
                }
            }
            //oneOfRule
            string[] oneOfVar1 = new string[0];
            string[] oneOfVar2 = new string[0];
            List<string> Propertylist3 = oneOfVar1.ToList();
            List<string> ObjectList3 = oneOfVar2.ToList();

            string State3 = "http://www.w3.org/2002/07/owl#oneOf";

            GetRule3(State3, Propertylist3, ObjectList3);

            oneOfVar1 = Propertylist3.ToArray();
            oneOfVar2 = ObjectList3.ToArray();

            value+= "\r\nResult of Inconsistency Check for OneOf Rule";
            for (int i = 0; i < oneOfVar1.Length; i++)
            {
                string Check = oneOfRule(oneOfVar1[i]);
                SparqlResultSet Nice3 = (SparqlResultSet)User_Interface.Sys_graph.ExecuteQuery(Check);

                foreach (SparqlResult result in Nice3)
                {
                    if (oneOfVar2[i].Contains(result["value"].ToString()))
                    {
                        value+= "";
                    }
                    else
                    {
                        value+= "\r\n!ERROR!\r\n";
                        value+= result["value"].ToString() + " in " + oneOfVar1[i].ToString() + " is an error!\r\n";
                    }
                }
            }
            //DisjointWith Rule
            string[] disjointWithVar1 = new string[0];
            string[] disjointWithVar2 = new string[0];
            List<string> Propertylist4 = disjointWithVar1.ToList();
            List<string> ObjectList4 = disjointWithVar2.ToList();

            string State4 = "http://www.w3.org/2002/07/owl#disjointWith";

            GetRule1(State4, Propertylist4, ObjectList4);

            disjointWithVar1 = Propertylist4.ToArray();
            disjointWithVar2 = ObjectList4.ToArray();

            value+= "\r\nResult of Inconsistency Check for disjointWith Rule";
            for (int i = 0; i < disjointWithVar1.Length; i++)
            {
                string Check = disjointWithRule(disjointWithVar1[i], disjointWithVar2[i]);
                SparqlResultSet Nice4 = (SparqlResultSet)User_Interface.Sys_graph.ExecuteQuery(Check);

                foreach (SparqlResult result in Nice4)
                {
                    if (result["value"].ToString() != "")
                    {
                        value+= "\r\n!ERROR!\r\n";
                        value+= result["value"].ToString() + " in " + disjointWithVar1[i].ToString() + " and " + disjointWithVar2[i] + " is an error!\r\n";
                    }
                }
            }
            //PropertyDisjointWith Rule
            string[] propdisjointWithVar1 = new string[0];
            string[] propdisjointWithVar2 = new string[0];
            List<string> Propertylist5 = propdisjointWithVar1.ToList();
            List<string> ObjectList5 = propdisjointWithVar2.ToList();

            string State5 = "http://www.w3.org/2002/07/owl#propertyDisjointWith";

            GetRule1(State5, Propertylist5, ObjectList5);

            propdisjointWithVar1 = Propertylist5.ToArray();
            propdisjointWithVar2 = ObjectList5.ToArray();

            value+= "\r\nResult of Inconsistency Check for Property disjointWith Rule";
            for (int i = 0; i < propdisjointWithVar1.Length; i++)
            {
                string Check = propertyDisjointWithRule(propdisjointWithVar1[i], propdisjointWithVar2[i]);
                SparqlResultSet Nice5 = (SparqlResultSet)User_Interface.Sys_graph.ExecuteQuery(Check);

                foreach (SparqlResult result in Nice5)
                {

                    if (result["value"].ToString() != "")
                    {
                        value+= "\r\n!ERROR!\r\n";
                        value+= result["value"].ToString() + " with predicate " + propdisjointWithVar1[i].ToString() + " and " + propdisjointWithVar2[i] + " is an error!\r\n";
                    }
                }
            }
            //Irreflexive Rule
            string[] IrreflexiveVar1 = new string[0];
            List<string> Propertylist6 = IrreflexiveVar1.ToList();

            string State6 = "http://www.w3.org/2002/07/owl#IrreflexiveProperty";
            GetRule2(State6, Propertylist6);

            IrreflexiveVar1 = Propertylist6.ToArray();

            value+= "\r\nResult of Inconsistency Check for Irreflexive Property Rule";
            for (int i = 0; i < IrreflexiveVar1.Length; i++)
            {
                string Check = IrreflexiveRule(IrreflexiveVar1[i]);

                SparqlResultSet Nice6 = (SparqlResultSet)User_Interface.Sys_graph.ExecuteQuery(Check);

                foreach (SparqlResult result in Nice6)
                {

                    if (result["value"].ToString() != "")
                    {
                        value+= "\r\n!ERROR!\r\n";
                        value+= result["value"].ToString() + " with predicate " + IrreflexiveVar1[i].ToString() + " is irreflexive!\r\n";
                    }
                }
            }

            //Asymmetric Property Rule
            string[] AsymmetricVar1 = new string[0];
            List<string> Propertylist7 = AsymmetricVar1.ToList();

            string State7 = "http://www.w3.org/2002/07/owl#AsymmetricProperty";
            GetRule2(State7, Propertylist7);

            AsymmetricVar1 = Propertylist7.ToArray();

            value+= "\r\nResult of Inconsistency Check for Asymmetric Property Rule";
            for (int i = 0; i < AsymmetricVar1.Length; i++)
            {
                string Check = AsymmetricRule(AsymmetricVar1[i]);

                SparqlResultSet Nice7 = (SparqlResultSet)User_Interface.Sys_graph.ExecuteQuery(Check);

                foreach (SparqlResult result in Nice7)
                {

                    if (result.ToString() != "")
                    {
                        value+= "\r\n!ERROR!\r\n";
                        value+= result["value"].ToString() + " and " + result["value1"].ToString() + " with predicate " + AsymmetricVar1[i].ToString() + " are asymmetric!\r\n";
                    }
                }
            }
            //Negative Property Assertion Rule
            string[] NPAssertVar1 = new string[0];
            string[] NPAssertVar2 = new string[0];
            string[] NPAssertVar3 = new string[0];
            List<string> Propertylist8 = NPAssertVar2.ToList();
            List<string> ObjectList8 = NPAssertVar3.ToList();
            List<string> Subjectlist8 = NPAssertVar1.ToList();

            GetRule4(Subjectlist8, Propertylist8, ObjectList8);

            NPAssertVar1 = Subjectlist8.ToArray();
            NPAssertVar2 = Propertylist8.ToArray();
            NPAssertVar3 = ObjectList8.ToArray();

            value+= "\r\nResult of Inconsistency Check for Negative Property Assertion Rule";
            for (int i = 0; i < NPAssertVar1.Length; i++)
            {
                string Check = NPAssertRule(NPAssertVar1[i], NPAssertVar2[i], NPAssertVar3[i]);

                SparqlResultSet Nice8 = (SparqlResultSet)User_Interface.Sys_graph.ExecuteQuery(Check);

                if (Nice8.Result == false)
                {
                    value+= "";
                }
                else
                {
                    value+= "\r\n!ERROR!\r\n";
                    value+= NPAssertVar1[i].ToString() + " with predicate " + NPAssertVar2[i].ToString() + " and object " + NPAssertVar3[i].ToString() + " is an error!\r\n";
                }

            }

            //All Disjoint Classes Rule
            string[] ADClassVar1 = new string[0];
            List<string> ObjectList9 = ADClassVar1.ToList();

            GetRule5(ObjectList9);

            ADClassVar1 = ObjectList9.ToArray();

            value+= "\r\nResult of Inconsistency Check for All Disjoint Classes Rule";
            for (int i = 0; i < ADClassVar1.Length; i += 3)
            {
                string Check = ADClassRule(ADClassVar1[i], ADClassVar1[i + 1], ADClassVar1[i + 2]);

                SparqlResultSet Nice9 = (SparqlResultSet)User_Interface.Sys_graph.ExecuteQuery(Check);

                foreach (SparqlResult result in Nice9)
                {

                    if (result.ToString() != "")
                    {
                        value+= "\r\n!ERROR!\r\n";
                        value+= result["value"].ToString() + " in " + ADClassVar1[i].ToString() + " and " + ADClassVar1[i + 1].ToString() + " and " + ADClassVar1[i + 2].ToString() + " is an error!\r\n";
                    }
                }
            }
            return value;
        }

        public Tuple<String, String, String> foo()
        {
            Tuple<String, String, String> tup = new Tuple<string, string, string>("val1", "val1", "val1");
            return tup;


        }

        public string SLRuleQuery1(string State) //this function calls the query
        {
            string query_1 =
                            "prefix ind:<URN:inds:>" +
                            "prefix prop:<URN:props:>" +
                            "prefix classes:<URN:class:>" +
                            "prefix rdfs:<http://www.w3.org/2000/01/rdf-schema#>" +
                            "prefix rdf:<http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                            "prefix owl:<http://www.w3.org/2002/07/owl#>" +
                    "select ?termA ?termB " +
                        "where {" +
                            "?termA <" + State + "> ?termB .}";
            return query_1;
        }

        public string SLRuleQuery2(string State) //this function calls the query
        {
            string query_12 =
                            "prefix ind:<URN:inds:>" +
                            "prefix prop:<URN:props:>" +
                            "prefix classes:<URN:class:>" +
                            "prefix rdfs:<http://www.w3.org/2000/01/rdf-schema#>" +
                            "prefix rdf:<http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                            "prefix owl:<http://www.w3.org/2002/07/owl#>" +
                    "select ?termA " +
                        "where {" +
                            "?termA  a <" + State + ">.}";
            return query_12;
        }

        public string SLRuleQuery3(string State) //this function calls the query
        {
            string query_13 =
                            "prefix ind:<URN:inds:>" +
                            "prefix prop:<URN:props:>" +
                            "prefix classes:<URN:class:>" +
                            "prefix rdfs:<http://www.w3.org/2000/01/rdf-schema#>" +
                            "prefix rdf:<http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                            "prefix owl:<http://www.w3.org/2002/07/owl#>" +
                    "select ?termA  ?termB " +
                        "where {" +
                            "?termA <" + State + "> ?termB .}";
            return query_13;
        }

        public string domainRule(string Prop) //this function calls the query
        {
            string query_2 =
                        "prefix ind:<URN:inds:>" +
                        "prefix prop:<URN:props:>" +
                        "prefix classes:<URN:class:>" +
                        "prefix rdfs:<http://www.w3.org/2000/01/rdf-schema#>" +
                "select ?sub ?value " +
                    "where {" +
                        "?sub <" + Prop + "> ?obj; " +
                        "           a    ?value.}";
            return query_2;
        }

        public string rangeRule(string Prop) //this function calls the query
        {
            string query_3 =
                            "prefix ind:<URN:inds:>" +
                            "prefix prop:<URN:props:>" +
                            "prefix classes:<URN:class:>" +
                            "prefix rdfs:<http://www.w3.org/2000/01/rdf-schema#>" +
                            "prefix rdf:<http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                            "prefix owl:<http://www.w3.org/2002/07/owl#>" +
                    "select ?object ?value " +
                        "where {" +
                             "?sub <" + Prop + "> ?object." +
                             " ?object  a  ?value .}";
            return query_3;
        }

        public string oneOfRule(string Obj) //this function calls the query
        {
            string query_4 =
                            "prefix ind:<URN:inds:>" +
                            "prefix prop:<URN:props:>" +
                            "prefix classes:<URN:class:>" +
                            "prefix rdfs:<http://www.w3.org/2000/01/rdf-schema#>" +
                            "prefix rdf:<http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                            "prefix owl:<http://www.w3.org/2002/07/owl#>" +
                    "select ?value " +
                        "where {" +
                             "?value a <" + Obj + ">.}";
            return query_4;
        }

        public string disjointWithRule(string obj1, string obj2) //this function calls the query
        {
            string query_5 =
                            "prefix ind:<URN:inds:>" +
                            "prefix prop:<URN:props:>" +
                            "prefix classes:<URN:class:>" +
                            "prefix rdfs:<http://www.w3.org/2000/01/rdf-schema#>" +
                            "prefix rdf:<http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                            "prefix owl:<http://www.w3.org/2002/07/owl#>" +
                    "select ?value " +
                        "where {" +
                             "?value a <" + obj1 + ">;" +
                             " a <" + obj2 + ">. }";
            return query_5;
        }

        public string propertyDisjointWithRule(string pred1, string pred2) //this function calls the query
        {
            string query_6 =
                            "prefix ind:<URN:inds:>" +
                            "prefix prop:<URN:props:>" +
                            "prefix classes:<URN:class:>" +
                            "prefix rdfs:<http://www.w3.org/2000/01/rdf-schema#>" +
                            "prefix rdf:<http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                            "prefix owl:<http://www.w3.org/2002/07/owl#>" +
                    "select ?value ?obj1 ?obj2 " +
                        "where {" +
                             "?value <" + pred1 + "> ?obj1;" +
                             " <" + pred2 + "> ?obj2. }";
            return query_6;
        }

        public string IrreflexiveRule(string pred1) //this function calls the query
        {
            string query_7 =
                            "prefix ind:<URN:inds:>" +
                            "prefix prop:<URN:props:>" +
                            "prefix classes:<URN:class:>" +
                            "prefix rdfs:<http://www.w3.org/2000/01/rdf-schema#>" +
                            "prefix rdf:<http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                            "prefix owl:<http://www.w3.org/2002/07/owl#>" +
                    "select ?value " +
                        "where {" +
                             "?value <" + pred1 + "> ?value.}";
            return query_7;
        }

        public string AsymmetricRule(string pred1) //this function calls the query
        {
            string query_8 =
                            "prefix ind:<URN:inds:>" +
                            "prefix prop:<URN:props:>" +
                            "prefix classes:<URN:class:>" +
                            "prefix rdfs:<http://www.w3.org/2000/01/rdf-schema#>" +
                            "prefix rdf:<http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                            "prefix owl:<http://www.w3.org/2002/07/owl#>" +
                    "select ?value ?value1 " +
                        "where {" +
                             "?value <" + pred1 + "> ?value1." +
                             "?value1 <" + pred1 + "> ?value.}";
            return query_8;
        }

        public string NPAssertRule(string sub, string pred, string obj) //this function calls the query
        {
            string query_9 =
                            "prefix ind:<URN:inds:>" +
                            "prefix prop:<URN:props:>" +
                            "prefix classes:<URN:class:>" +
                            "prefix rdfs:<http://www.w3.org/2000/01/rdf-schema#>" +
                            "prefix rdf:<http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                            "prefix owl:<http://www.w3.org/2002/07/owl#>" +
                            "prefix xml:<http://www.w3.org/2001/XMLSchema#integer>" +
                    "ask " +
                        " {" +
                             "<" + sub + "> <" + pred + "> <" + obj + ">. }";
            return query_9;
        }

        public string ADClassRule(string obj1, string obj2, string obj3) //this function calls the query
        {
            string query_5 =
                            "prefix ind:<URN:inds:>" +
                            "prefix prop:<URN:props:>" +
                            "prefix classes:<URN:class:>" +
                            "prefix rdfs:<http://www.w3.org/2000/01/rdf-schema#>" +
                            "prefix rdf:<http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                            "prefix owl:<http://www.w3.org/2002/07/owl#>" +
                    "select ?value " +
                        "where {" +
                             "?value a <" + obj1 + ">;" +
                             " a <" + obj2 + ">;" +
                             " a <" + obj3 + ">. }";
            return query_5;
        }

        private void GetRule1(string State, List<string> Subjectlist, List<string> Objectlist)
        {
            string forRule = SLRuleQuery1(State);
            SparqlResultSet Nice1 = (SparqlResultSet)User_Interface.Sys_rule.ExecuteQuery(forRule);

            foreach (SparqlResult result in Nice1)
            {
                INode Prop = result["termA"];
                INode Obj = result["termB"];
                Subjectlist.Add(Prop.ToString());
                Objectlist.Add(Obj.ToString());
            }
        }

        private void GetRule2(string State, List<string> Subjectlist)
        {
            string forRule1 = SLRuleQuery2(State);
            SparqlResultSet Nice12 = (SparqlResultSet)User_Interface.Sys_rule.ExecuteQuery(forRule1);

            foreach (SparqlResult result in Nice12)
            {
                INode Prop = result["termA"];
                Subjectlist.Add(Prop.ToString());
            }
        }

        private void GetRule3(string State, List<string> Subjectlist, List<string> Objectlist)
        {
            string forRule = SLRuleQuery3(State);
            SparqlResultSet Nice1 = (SparqlResultSet)User_Interface.Sys_rule.ExecuteQuery(forRule);

            foreach (SparqlResult result in Nice1)
            {
                INode Prop = result["termA"];
                INode Obj = result["termB"];
                Subjectlist.Add(Prop.ToString());
                Objectlist.Add(Obj.ToString());
            }
        }

        private void GetRule4(List<string> Subjectlist, List<string> Predlist, List<string> Objectlist)
        {
            string query_14 =
                           "prefix ind:<URN:inds:>" +
                           "prefix prop:<URN:props:>" +
                           "prefix classes:<URN:class:>" +
                           "prefix rdfs:<http://www.w3.org/2000/01/rdf-schema#>" +
                           "prefix rdf:<http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                           "prefix owl:<http://www.w3.org/2002/07/owl#>" +
                   "select ?val1 ?val2 ?val3 " +
                       "where {" +
                           "?sub a owl:NegativePropertyAssertion;" +
                              " owl:sourceIndividual ?val1;" +
                              " owl:assertionProperty ?val2;" +
                              " owl:target            ?val3.}";


            SparqlResultSet Nice1 = (SparqlResultSet)User_Interface.Sys_rule.ExecuteQuery(query_14);

            foreach (SparqlResult result in Nice1)
            {
                INode Sub = result["val1"];
                INode Prop = result["val2"];
                INode Obj = result["val3"];
                Subjectlist.Add(Sub.ToString());
                Predlist.Add(Prop.ToString());
                Objectlist.Add(Obj.ToString());
            }
        }
        private void GetRule5(List<string> Objectlist)
        {
            string query_14 =
                           "prefix ind:<URN:inds:>" +
                           "prefix prop:<URN:props:>" +
                           "prefix classes:<URN:class:>" +
                           "prefix rdfs:<http://www.w3.org/2000/01/rdf-schema#>" +
                           "prefix rdf:<http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                           "prefix owl:<http://www.w3.org/2002/07/owl#>" +
                   "select ?val1 " +
                       "where {" +
                           "?sub a owl:AllDisjointClasses;" +
                              " owl:members ?val1.} ";

            SparqlResultSet Nice1 = (SparqlResultSet)User_Interface.Sys_rule.ExecuteQuery(query_14);

            foreach (SparqlResult result in Nice1)
            {
                INode Obj = result["val1"];
                Objectlist.Add(Obj.ToString());
            }
        }

    }
}
