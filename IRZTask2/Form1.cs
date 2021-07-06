using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRZTask2
{
    public partial class Form1 : Form
    {
        private string page;
        private string pagesize;
        private string fromdate;
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            page = "page=" + textBox2.Text + "&";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            pagesize = "pagesize=" + textBox1.Text + "&";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            fromdate = "fromdate=" + textBox3.Text + "&";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PostRequest(page, pagesize, fromdate);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("column1", "Creation date"); // Creating table columns.
            dataGridView1.Columns.Add("column2", "Title");
            dataGridView1.Columns.Add("column3", "Author");
            dataGridView1.Columns.Add("column4", "Answered?");
            dataGridView1.Columns.Add("column5", "Link");
            try
            {
                using (StreamReader sr = new StreamReader("MyJson.txt")) // Reading JSON from a file.
                {
                    Question question = JsonConvert.DeserializeObject<Question>(sr.ReadToEnd()); // Deserialization of JSON in Question.
                    for (int i = 0; i < question.items.Length; i++) // Filling the table with data.
                    {
                        dataGridView1.Rows.Add(question.items[i].creation_date, question.items[i].title, question.items[i].owner.display_name, question.items[i].is_answered, question.items[i].link);
                    }
                }
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private static void PostRequest(string page, string pagesize, string fromdate)
        {
            string uri = "https://api.stackexchange.com/2.2/search?" + page + pagesize + fromdate +"order=desc&sort=activity&intitle=beautiful&site=stackoverflow"; // Addres.
            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest; // Creating a WebRequest object.
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using (WebResponse response = request.GetResponse()) // Sending a request and receiving a response.
            {
                StreamReader reader = new StreamReader(response.GetResponseStream()); // Getting the response stream and manipulating it.
                try
                {
                    using (StreamWriter sw = new StreamWriter("MyJson.txt", true)) // Writing the received response to a file.
                    {
                        sw.WriteLine(reader.ReadToEnd());
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
    }
}