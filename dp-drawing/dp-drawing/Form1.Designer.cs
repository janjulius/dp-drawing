﻿namespace dp_drawing
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.canvas = new System.Windows.Forms.Panel();
            this.colorpicker = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.AccessibleName = "MainCanvas";
            this.canvas.BackColor = System.Drawing.SystemColors.Info;
            this.canvas.Location = new System.Drawing.Point(12, 39);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(776, 399);
            this.canvas.TabIndex = 0;
            this.canvas.Click += new System.EventHandler(this.canvas_Click);
            // 
            // colorpicker
            // 
            this.colorpicker.Location = new System.Drawing.Point(12, 10);
            this.colorpicker.Name = "colorpicker";
            this.colorpicker.Size = new System.Drawing.Size(75, 23);
            this.colorpicker.TabIndex = 0;
            this.colorpicker.Text = "Colorpicker";
            this.colorpicker.UseVisualStyleBackColor = true;
            this.colorpicker.Click += new System.EventHandler(this.colorpicker_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox1.Location = new System.Drawing.Point(93, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(20, 20);
            this.textBox1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.colorpicker);
            this.Controls.Add(this.canvas);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel canvas;
        private System.Windows.Forms.Button colorpicker;
        private System.Windows.Forms.TextBox textBox1;
    }
}
