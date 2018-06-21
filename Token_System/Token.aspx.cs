using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Token_System
{
    public partial class Token : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TokenQueue"] == null)
            {
                Queue<int> queueTokens = new Queue<int>();
                Session["TokenQueue"] = queueTokens;
            }
        }

        private void AddTokensToListBox(Queue<int> tokenQueue)
        {
            lstToken.Items.Clear();
            foreach (int token in tokenQueue)
            {
                lstToken.Items.Add(token.ToString());
            }
        }

        private void ServeNextCustomer(TextBox textBox, int counterNumber)
        {
            Queue<int> tokenQueue = (Queue<int>)Session["TokenQueue"];
            if (tokenQueue.Count == 0)
            {
                textBox.Text = "No Customer in the Queue..";
            }
            else
            {
                int tokenNumberToBeServed = tokenQueue.Dequeue();
                textBox.Text = tokenNumberToBeServed.ToString();
                txtDisplay.Text = "Token Number " + tokenNumberToBeServed.ToString() + " Please go to Counter " + counterNumber;
                AddTokensToListBox(tokenQueue);
            }
        }

        protected void btnCounter1_Click(object sender, EventArgs e)
        {
            ServeNextCustomer(txtCounter1, 1);
        }

        protected void btnCounter2_Click(object sender, EventArgs e)
        {
            ServeNextCustomer(txtCounter2, 2);
        }

        protected void btnCounter3_Click(object sender, EventArgs e)
        {
            ServeNextCustomer(txtCounter3, 3);
        }

        protected void btnPrintToken_Click(object sender, EventArgs e)
        {
            Queue<int> tokenQueue = (Queue<int>)Session["TokenQueue"];
            lblMsg.Text = "There are " + tokenQueue.Count.ToString() + " Customers before you in the Queue..";

            if (Session["LastTokenNumberIssued"] == null)
            {
                Session["LastTokenNumberIssued"] = 0;
            }

            int nextTokenNumberToBeIssued = (int)Session["LastTokenNumberIssued"] + 1;
            Session["LastTokenNumberIssued"] = nextTokenNumberToBeIssued;
            tokenQueue.Enqueue(nextTokenNumberToBeIssued);
            AddTokensToListBox(tokenQueue);
        }
    }
}