namespace Second
{
    partial class AddMaterial
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
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonAccept = new System.Windows.Forms.Button();
            this.TextBoxResistance = new System.Windows.Forms.TextBox();
            this.TextBoxName = new System.Windows.Forms.TextBox();
            this.LabelResistance = new System.Windows.Forms.Label();
            this.LabelName = new System.Windows.Forms.Label();
            this.UnFocus = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(210, 64);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 11;
            this.ButtonCancel.Text = "Отмена";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonAccept
            // 
            this.ButtonAccept.Location = new System.Drawing.Point(12, 64);
            this.ButtonAccept.Name = "ButtonAccept";
            this.ButtonAccept.Size = new System.Drawing.Size(75, 23);
            this.ButtonAccept.TabIndex = 10;
            this.ButtonAccept.Text = "Ок";
            this.ButtonAccept.UseVisualStyleBackColor = true;
            this.ButtonAccept.Click += new System.EventHandler(this.ButtonAccept_Click);
            // 
            // TextBoxResistance
            // 
            this.TextBoxResistance.Location = new System.Drawing.Point(145, 38);
            this.TextBoxResistance.Name = "TextBoxResistance";
            this.TextBoxResistance.Size = new System.Drawing.Size(117, 20);
            this.TextBoxResistance.TabIndex = 9;
            this.TextBoxResistance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxResistance_KeyPress);
            // 
            // TextBoxName
            // 
            this.TextBoxName.Location = new System.Drawing.Point(145, 12);
            this.TextBoxName.Name = "TextBoxName";
            this.TextBoxName.Size = new System.Drawing.Size(117, 20);
            this.TextBoxName.TabIndex = 8;
            this.TextBoxName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxName_KeyPress);
            // 
            // LabelResistance
            // 
            this.LabelResistance.AutoSize = true;
            this.LabelResistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelResistance.Location = new System.Drawing.Point(12, 38);
            this.LabelResistance.Name = "LabelResistance";
            this.LabelResistance.Size = new System.Drawing.Size(133, 20);
            this.LabelResistance.TabIndex = 7;
            this.LabelResistance.Text = "Сопротивление:";
            // 
            // LabelName
            // 
            this.LabelName.AutoSize = true;
            this.LabelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelName.Location = new System.Drawing.Point(12, 12);
            this.LabelName.Name = "LabelName";
            this.LabelName.Size = new System.Drawing.Size(87, 20);
            this.LabelName.TabIndex = 6;
            this.LabelName.Text = "Название:";
            // 
            // UnFocus
            // 
            this.UnFocus.Location = new System.Drawing.Point(210, 26);
            this.UnFocus.Name = "UnFocus";
            this.UnFocus.Size = new System.Drawing.Size(0, 0);
            this.UnFocus.TabIndex = 12;
            this.UnFocus.UseVisualStyleBackColor = true;
            // 
            // AddMaterial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 99);
            this.Controls.Add(this.UnFocus);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonAccept);
            this.Controls.Add(this.TextBoxResistance);
            this.Controls.Add(this.TextBoxName);
            this.Controls.Add(this.LabelResistance);
            this.Controls.Add(this.LabelName);
            this.MaximumSize = new System.Drawing.Size(313, 137);
            this.MinimumSize = new System.Drawing.Size(313, 137);
            this.Name = "AddMaterial";
            this.Text = "Добавление нового материала";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonAccept;
        private System.Windows.Forms.TextBox TextBoxResistance;
        private System.Windows.Forms.TextBox TextBoxName;
        private System.Windows.Forms.Label LabelResistance;
        private System.Windows.Forms.Label LabelName;
        private System.Windows.Forms.Button UnFocus;
    }
}