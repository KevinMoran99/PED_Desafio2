using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace PED_Desafio2_MA171622_MG171623
{
    public partial class Form1 : Form
    {
        Stack<char> inStack = new Stack<char>();
        Stack<char> outStack = new Stack<char>();
        List<char> allData = new List<char>();
        DibujaALV arbolALV = new DibujaALV(null);
        Graphics g;


        char dato = ' ';
        char datb = ' ';
        
        int timerCont = 0;

        Thread helperThread;

        public Form1()
        {
            InitializeComponent();
        }

        //Updating the lstInStack with the items contained vy inStack
        private void refreshInList()
        {
            lstInStack.Items.Clear();
            foreach (char letter in inStack)
            {
                lstInStack.Items.Add(letter);
            }
            if (lstInStack.Items.Count > 0)
            {
                lstInStack.SelectedIndex = 0;
            }
            lstInStack.Refresh();
        }

        private void refreshOutList()
        {
            lstOutStack.Items.Clear();
            foreach (char letter in outStack)
            {
                lstOutStack.Items.Add(letter);
            }
            if (lstOutStack.Items.Count > 0)
            {
                lstOutStack.SelectedIndex = 0;
            }
            lstOutStack.Refresh();
        }

        //Switching between the buttons being enabled or disabled
        private void toggleButtons()
        {
            foreach (Control c in grbKeyboard.Controls)
            {
                try
                {
                    ((Button)c).Enabled = !((Button)c).Enabled;
                }
                catch { }
            }

            btnPop.Enabled = !btnPop.Enabled;
        }

        //Triggers the animation of the letter moving between TADs
        //initialX must be 30 to move from inStack to the tree, and 550 to move between the tree to outStack
        private void moveLetter(char letter, int initialX)
        {
            lblMove.Text = letter.ToString();
            lblMove.Location = new Point(initialX, 190);
            lblMove.Visible = true;
            timer1.Enabled = true;
        }

        //Virtual keyboard buttons handler
        private void keyboard_Click(object sender, EventArgs e)
        {
            inStack.Push(((Button)sender).Text[0]);

            refreshInList();
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void btnPop_Click(object sender, EventArgs e)
        {
            try
            {
                
                //Pops the inStack
                dato = inStack.Pop();
                refreshInList();
                allData.Add(dato);
                //Disables all buttons
                toggleButtons();

                //Begins letter move animation
                moveLetter(dato, 30);

                //Adds the letter in a different thread
                //This thread is started once the moveLetter animation ends
                helperThread = new Thread(delegate() { arbolALV.Insertar(dato); });
                
            }
            catch
            {
                MessageBox.Show("La pila de entrada está vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pnlArbol_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g = e.Graphics;

            arbolALV.DibujarArbol(g, this.Font, Brushes.White, Brushes.Black, Pens.White, datb, Brushes.Black);
            datb = ' ';
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Moves the letter
            lblMove.Location = new Point(lblMove.Location.X + 5, 190);
            timerCont++;

            //Once the animation has ended, the timer is disabled
            if(timerCont >= 105)
            {
                lblMove.Visible = false;
                timer1.Enabled = false;
                timerCont = 0;
                //Starting the helperThread that triggers the action that was waiting for the animation to end
                helperThread.Start();
                //Enables all buttons
                toggleButtons();
                Refresh();
                Refresh();
                refreshOutList();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void txtDelete_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                dato = char.Parse(txtDelete.Text.ToUpper());
                bool flag = false;

                foreach(char c in allData)
                {
                    if(c == dato)
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    arbolALV.Eliminar(dato);
                    Refresh();

                    //Disables all buttons
                    toggleButtons();
                    
                    //Begins letter move animation
                    moveLetter(dato, 550);

                    allData.Remove(dato);
                    //Adds the letter in a different thread
                    //This thread is started once the moveLetter animation ends
                    helperThread = new Thread(delegate () { outStack.Push(dato); });
                }

                txtDelete.Clear();
            
            }
            catch
            {
                Refresh();
                Refresh();
                MessageBox.Show("No se encontro el dato especificado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
