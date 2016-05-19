namespace Second
{
    partial class SetValue
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
            this.LabelXCoordinate = new System.Windows.Forms.Label();
            this.LabelYCoordinate = new System.Windows.Forms.Label();
            this.TextBoxXCoordinate = new System.Windows.Forms.TextBox();
            this.TextBoxYCoordinate = new System.Windows.Forms.TextBox();
            this.ButtonAccept = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.UnFocus = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelXCoordinate
            // 
            this.LabelXCoordinate.AutoSize = true;
            this.LabelXCoordinate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelXCoordinate.Location = new System.Drawing.Point(44, 12);
            this.LabelXCoordinate.Name = "LabelXCoordinate";
            this.LabelXCoordinate.Size = new System.Drawing.Size(24, 20);
            this.LabelXCoordinate.TabIndex = 0;
            this.LabelXCoordinate.Text = "X:";
            // 
            // LabelYCoordinate
            // 
            this.LabelYCoordinate.AutoSize = true;
            this.LabelYCoordinate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelYCoordinate.Location = new System.Drawing.Point(44, 38);
            this.LabelYCoordinate.Name = "LabelYCoordinate";
            this.LabelYCoordinate.Size = new System.Drawing.Size(24, 20);
            this.LabelYCoordinate.TabIndex = 1;
            this.LabelYCoordinate.Text = "Y:";
            // 
            // TextBoxXCoordinate
            // 
            this.TextBoxXCoordinate.Location = new System.Drawing.Point(74, 12);
            this.TextBoxXCoordinate.Name = "TextBoxXCoordinate";
            this.TextBoxXCoordinate.Size = new System.Drawing.Size(138, 20);
            this.TextBoxXCoordinate.TabIndex = 2;
            this.TextBoxXCoordinate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxXCoordinate_KeyPress);
            // 
            // TextBoxYCoordinate
            // 
            this.TextBoxYCoordinate.Location = new System.Drawing.Point(74, 38);
            this.TextBoxYCoordinate.Name = "TextBoxYCoordinate";
            this.TextBoxYCoordinate.Size = new System.Drawing.Size(138, 20);
            this.TextBoxYCoordinate.TabIndex = 3;
            this.TextBoxYCoordinate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxYCoordinate_KeyPress);
            // 
            // ButtonAccept
            // 
            this.ButtonAccept.Location = new System.Drawing.Point(12, 64);
            this.ButtonAccept.Name = "ButtonAccept";
            this.ButtonAccept.Size = new System.Drawing.Size(75, 23);
            this.ButtonAccept.TabIndex = 4;
            this.ButtonAccept.Text = "Ок";
            this.ButtonAccept.UseVisualStyleBackColor = true;
            this.ButtonAccept.Click += new System.EventHandler(this.ButtonAccept_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(166, 64);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 5;
            this.ButtonCancel.Text = "Отмена";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // UnFocus
            // 
            this.UnFocus.Location = new System.Drawing.Point(13, 15);
            this.UnFocus.Name = "UnFocus";
            this.UnFocus.Size = new System.Drawing.Size(0, 0);
            this.UnFocus.TabIndex = 6;
            this.UnFocus.Text = "UnFocus";
            this.UnFocus.UseVisualStyleBackColor = true;
            // 
            // SetValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 99);
            this.Controls.Add(this.UnFocus);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonAccept);
            this.Controls.Add(this.TextBoxYCoordinate);
            this.Controls.Add(this.TextBoxXCoordinate);
            this.Controls.Add(this.LabelYCoordinate);
            this.Controls.Add(this.LabelXCoordinate);
            this.MaximumSize = new System.Drawing.Size(269, 137);
            this.MinimumSize = new System.Drawing.Size(269, 137);
            this.Name = "SetValue";
            this.Text = "Задать значение точки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelXCoordinate;
        private System.Windows.Forms.Label LabelYCoordinate;
        private System.Windows.Forms.TextBox TextBoxXCoordinate;
        private System.Windows.Forms.TextBox TextBoxYCoordinate;
        private System.Windows.Forms.Button ButtonAccept;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button UnFocus;
    }
}