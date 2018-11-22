using PORTAL.DAL;
using PORTAL.MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple__CRUD_WithMySql
{
    public partial class FrmUpdateItem : Form
    {
        //create item object
        item Item = new item();

        public FrmUpdateItem()
        {
            InitializeComponent();
        }

        public FrmUpdateItem(item Itm)
        {
            InitializeComponent();
            Item = Itm;
        }

        private void FrmUpdateItem_Load(object sender, EventArgs e)
        {
            try
            {
                txtProductName.Text = Item.ProductName;
                txtbPrice.Text = Item.Price.ToString();
                chkActive.Checked = Item.isActive;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtbPrice.Text != string.Empty && txtProductName.Text != string.Empty) //check if all fields are filled
                {
                    string isActive = chkActive.Checked ? "1" : "0";
                    string sqlQuery = "UPDATE items SET productName = '" + txtProductName.Text + "', productPrice = '" + txtbPrice.Text + "', isActive = '" + isActive + "'  WHERE ID = '" + Item.ID + "';";

                    //update data
                    MySQLconnection con = new MySQLconnection();
                    con.Executing(sqlQuery);

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtbPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
