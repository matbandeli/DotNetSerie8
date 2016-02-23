using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace serie8_partie2
{
    public partial class Form1 : Form
    {
        private const string spacing = "    ";
        private XmlDocument doc;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML file (*.xml) | *.xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                doc = new XmlDocument();
                doc.Load(openFileDialog.FileName);

                UpdateXmlDisplay();                
            }
            openFileDialog.Dispose();
        }

        private void parse(XmlNode node, int deepness)
        {
            string tabulation = "";
            for(int i = 0; i < deepness; i++)
                tabulation += spacing;            

            if (node.HasChildNodes)
            {
                string attributes = "";
                foreach(XmlAttribute xmlAtribute in node.Attributes)
                {
                    attributes += xmlAtribute.OuterXml + " ";
                }

                textBox1.AppendText(tabulation + "<" + node.Name + " " + attributes + ">" + Environment.NewLine);                
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    parse(childNode, deepness + 1);
                }
                textBox1.AppendText(tabulation + "</" + node.Name + ">" + Environment.NewLine);
            }
            else
            {
                textBox1.AppendText(tabulation + node.InnerText + Environment.NewLine);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = "xml";
            if (save.ShowDialog() == DialogResult.OK)
            {
                doc.Save(save.FileName);
            }
            save.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            XmlElement racine = doc.CreateElement("livre");

            //Titre
            XmlElement eltTitre = doc.CreateElement("Titre");
            XmlText titre = doc.CreateTextNode(textBox2.Text);
            eltTitre.AppendChild(titre);

            //Auteur
            XmlElement eltAuteur = doc.CreateElement("Auteur");
            XmlElement eltNom = doc.CreateElement("Nom");
            XmlElement eltNatonalite = doc.CreateElement("Nationalite");

            XmlText nom = doc.CreateTextNode(textBox3.Text);
            eltNom.AppendChild(nom);

            XmlText nation = doc.CreateTextNode(textBox4.Text);
            eltNatonalite.AppendChild(nation);

            eltAuteur.AppendChild(eltNom);
            eltAuteur.AppendChild(eltNatonalite);

            //Langue
            XmlElement eltLangue = doc.CreateElement("Langue");
            XmlText langue = doc.CreateTextNode(textBox5.Text);
            eltLangue.AppendChild(langue);


            racine.AppendChild(eltTitre);
            racine.AppendChild(eltAuteur);
            racine.AppendChild(eltLangue);

            doc.DocumentElement.AppendChild(racine);

            UpdateXmlDisplay();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (doc.DocumentElement.HasChildNodes)
            {
                doc.DocumentElement.RemoveChild(doc.DocumentElement.LastChild);                
            }                

            UpdateXmlDisplay();
        }

        private void UpdateXmlDisplay()
        {
            textBox1.Clear();
            parse(doc.DocumentElement, 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox7.Clear();

            XmlNodeList matchNodes = doc.SelectNodes(textBox6.Text);

            foreach(XmlNode matchNode in matchNodes)
            {
                textBox7.AppendText(matchNode.InnerXml + Environment.NewLine);
            }
        }
    }
}
