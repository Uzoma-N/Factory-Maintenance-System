using System;
using System.Collections.Generic;

using System.Windows.Forms;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;

namespace Maintenance_system
{
    public partial class formInconsistency : Form
    {
        public formInconsistency()
        {
            InitializeComponent();
        }
        Inconsistency_Check InconsistencyObject = new Inconsistency_Check();
        private void formInconsistency_Load(object sender, EventArgs e)
        {
            icheckDetail.Text = InconsistencyObject.Checks();
        }

        private void btnClearCheck_Click_1(object sender, EventArgs e)
        {
            icheckDetail.Text = "";
        }
    }
}
