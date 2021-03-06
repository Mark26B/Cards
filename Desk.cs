﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Cards
{
    public partial class Desk : Form
    {
        private string folderPath = null;
        private string[] fileNames = null;
        private Random rand = new Random();

        private List<PictureBox> cardImages = new List<PictureBox>();

        private bool mouseHold = false;
        private int deltaX;
        private int deltaY; 

        public Desk()
        {
            InitializeComponent();
            InitializeDesk();
        }

        private void InitializeDesk()
        {
            this.BackColor = Color.Green;
        }

        private string SelectFolder()
        {
            var selectFolderDialog = new FolderBrowserDialog();
            DialogResult result = selectFolderDialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(selectFolderDialog.SelectedPath))
            {
                return selectFolderDialog.SelectedPath;
            }
            return null;
        }

        private void LoadCards_Click(object sender, EventArgs e)
        {
            PictureBox filePictureBox = null;

            folderPath = @"C:\Users\Marks\Downloads\Playing Cards\Playing Cards\playing_card_images\face";
            //folderPath = SelectFolder();
            if(folderPath == null)
            {
                return;
            }
            //else if(folderPath !== )

            fileNames = Directory.GetFiles(folderPath);


            foreach(var fileName in fileNames)
            {
                filePictureBox = new PictureBox()
                {
                    Height = 110,
                    Width = 70,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Left = rand.Next(0, 400),
                    Top = rand.Next(40, 250),
                    Image = Image.FromFile(fileName)
                };
                filePictureBox.MouseDoubleClick += Card_DoubleClick;
                filePictureBox.MouseDown += Card_MouseDown;
                filePictureBox.MouseUp += Card_MouseUp;
                filePictureBox.MouseMove += Card_MouseMove;
                this.Controls.Add(filePictureBox);
                cardImages.Add(filePictureBox);
            }
        }

        private void StackCards_Click(object sender, EventArgs e)
        {
            int x = 150, y = 150;
            foreach(var card in cardImages)
            {
                card.Location = new Point(x, y);
                x++;
                y++;
            }
        }

        private void DeckCards_Click(object sender, EventArgs e)
        {
            int counter = 0;
            for(int x = 1; x < 10; x++)
            {
                for(int y = 1; y < 7; y++)
                {
                    cardImages[counter].Location = new Point(x * 75, y * 115);
                    counter++;
                }
            }
        }

        private void Card_DoubleClick(object sender, EventArgs e)
        {
            var card = (PictureBox)sender;
            card.Location = new Point(2, 28);
            card.BringToFront();
        }

        private void Card_MouseDown(object sender, MouseEventArgs e)
        {
            var card = (PictureBox)sender;
            card.BringToFront();
            if (e.Button == MouseButtons.Left)
            {
                mouseHold = true;
                deltaX = e.X;
                deltaY = e.Y;
            }
        }

        private void Card_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseHold = false;
            }
        }

        private void Card_MouseMove(object sender, MouseEventArgs e)
        {
            var card = (PictureBox)sender;
            if (!mouseHold)
            {
                return;
            }
            card.Top = e.Y + card.Top - deltaY;
            card.Left = e.X + card.Left - deltaX;
        }
    }
}
