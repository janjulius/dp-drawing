namespace dp_drawing
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
            System.Windows.Forms.Button MakeGroupButton;
            this.canvas = new System.Windows.Forms.Panel();
            this.colorpicker = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rectButton = new System.Windows.Forms.Button();
            this.ellipseButton = new System.Windows.Forms.Button();
            this.UndoButton = new System.Windows.Forms.Button();
            this.RedoButton = new System.Windows.Forms.Button();
            this.AddOrnament = new System.Windows.Forms.Button();
            this.OrnamentTextBox = new System.Windows.Forms.TextBox();
            this.OrnamentComboBox = new System.Windows.Forms.ComboBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            MakeGroupButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MakeGroupButton
            // 
            MakeGroupButton.Location = new System.Drawing.Point(930, 7);
            MakeGroupButton.Name = "MakeGroupButton";
            MakeGroupButton.Size = new System.Drawing.Size(75, 23);
            MakeGroupButton.TabIndex = 4;
            MakeGroupButton.Text = "Make Group";
            MakeGroupButton.UseVisualStyleBackColor = true;
            MakeGroupButton.Click += new System.EventHandler(this.MakeGroupButton_Click);
            // 
            // canvas
            // 
            this.canvas.AccessibleName = "MainCanvas";
            this.canvas.BackColor = System.Drawing.Color.Silver;
            this.canvas.Location = new System.Drawing.Point(12, 39);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(1155, 523);
            this.canvas.TabIndex = 0;
            this.canvas.Click += new System.EventHandler(this.canvas_Click);
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
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
            // rectButton
            // 
            this.rectButton.Location = new System.Drawing.Point(119, 10);
            this.rectButton.Name = "rectButton";
            this.rectButton.Size = new System.Drawing.Size(23, 23);
            this.rectButton.TabIndex = 0;
            this.rectButton.Text = "▯";
            this.rectButton.UseVisualStyleBackColor = true;
            this.rectButton.Click += new System.EventHandler(this.rectButton_Click);
            // 
            // ellipseButton
            // 
            this.ellipseButton.Location = new System.Drawing.Point(148, 10);
            this.ellipseButton.Name = "ellipseButton";
            this.ellipseButton.Size = new System.Drawing.Size(23, 23);
            this.ellipseButton.TabIndex = 1;
            this.ellipseButton.Text = "○";
            this.ellipseButton.UseVisualStyleBackColor = true;
            this.ellipseButton.Click += new System.EventHandler(this.ellipseButton_Click);
            // 
            // UndoButton
            // 
            this.UndoButton.Location = new System.Drawing.Point(1092, 8);
            this.UndoButton.Name = "UndoButton";
            this.UndoButton.Size = new System.Drawing.Size(75, 23);
            this.UndoButton.TabIndex = 2;
            this.UndoButton.Text = "Undo";
            this.UndoButton.UseVisualStyleBackColor = true;
            this.UndoButton.Click += new System.EventHandler(this.UndoButton_Click);
            // 
            // RedoButton
            // 
            this.RedoButton.Location = new System.Drawing.Point(1011, 7);
            this.RedoButton.Name = "RedoButton";
            this.RedoButton.Size = new System.Drawing.Size(75, 23);
            this.RedoButton.TabIndex = 3;
            this.RedoButton.Text = "Redo";
            this.RedoButton.UseVisualStyleBackColor = true;
            this.RedoButton.Click += new System.EventHandler(this.RedoButton_Click);
            // 
            // AddOrnament
            // 
            this.AddOrnament.Location = new System.Drawing.Point(834, 7);
            this.AddOrnament.Name = "AddOrnament";
            this.AddOrnament.Size = new System.Drawing.Size(90, 23);
            this.AddOrnament.TabIndex = 5;
            this.AddOrnament.Text = "Add Ornament";
            this.AddOrnament.UseVisualStyleBackColor = true;
            this.AddOrnament.Click += new System.EventHandler(this.AddOrnament_Click);
            // 
            // OrnamentTextBox
            // 
            this.OrnamentTextBox.Location = new System.Drawing.Point(460, 7);
            this.OrnamentTextBox.Name = "OrnamentTextBox";
            this.OrnamentTextBox.Size = new System.Drawing.Size(241, 20);
            this.OrnamentTextBox.TabIndex = 6;
            this.OrnamentTextBox.Text = "Yeet";
            this.OrnamentTextBox.TextChanged += new System.EventHandler(this.OrnamentTextBox_TextChanged);
            // 
            // OrnamentComboBox
            // 
            this.OrnamentComboBox.FormattingEnabled = true;
            this.OrnamentComboBox.Items.AddRange(new object[] {
            "Top",
            "Right",
            "Bottom",
            "Left"});
            this.OrnamentComboBox.Location = new System.Drawing.Point(707, 7);
            this.OrnamentComboBox.Name = "OrnamentComboBox";
            this.OrnamentComboBox.Size = new System.Drawing.Size(121, 21);
            this.OrnamentComboBox.TabIndex = 7;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(379, 4);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 8;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(298, 4);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LoadButton.Size = new System.Drawing.Size(75, 23);
            this.LoadButton.TabIndex = 9;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 574);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.OrnamentComboBox);
            this.Controls.Add(this.OrnamentTextBox);
            this.Controls.Add(this.AddOrnament);
            this.Controls.Add(MakeGroupButton);
            this.Controls.Add(this.RedoButton);
            this.Controls.Add(this.UndoButton);
            this.Controls.Add(this.ellipseButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.rectButton);
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
        private System.Windows.Forms.Button rectButton;
        private System.Windows.Forms.Button ellipseButton;
        private System.Windows.Forms.Button UndoButton;
        private System.Windows.Forms.Button RedoButton;
        private System.Windows.Forms.Button AddOrnament;
        private System.Windows.Forms.TextBox OrnamentTextBox;
        private System.Windows.Forms.ComboBox OrnamentComboBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button LoadButton;
    }
}

