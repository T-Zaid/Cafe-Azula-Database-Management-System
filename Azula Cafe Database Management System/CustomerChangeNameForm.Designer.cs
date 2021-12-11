
namespace Azula_Cafe_Database_Management_System
{
    partial class CustomerChangeNameForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.NewNameTextBox = new System.Windows.Forms.TextBox();
            this.UpdateNameButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter New Name:";
            // 
            // NewNameTextBox
            // 
            this.NewNameTextBox.Location = new System.Drawing.Point(59, 42);
            this.NewNameTextBox.Name = "NewNameTextBox";
            this.NewNameTextBox.Size = new System.Drawing.Size(153, 20);
            this.NewNameTextBox.TabIndex = 1;
            this.NewNameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NewNameTextBox_KeyPress);
            // 
            // UpdateNameButton
            // 
            this.UpdateNameButton.Location = new System.Drawing.Point(97, 86);
            this.UpdateNameButton.Name = "UpdateNameButton";
            this.UpdateNameButton.Size = new System.Drawing.Size(75, 23);
            this.UpdateNameButton.TabIndex = 2;
            this.UpdateNameButton.Text = "Update";
            this.UpdateNameButton.UseVisualStyleBackColor = true;
            this.UpdateNameButton.Click += new System.EventHandler(this.UpdateNameButton_Click);
            // 
            // CustomerChangeNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 121);
            this.Controls.Add(this.UpdateNameButton);
            this.Controls.Add(this.NewNameTextBox);
            this.Controls.Add(this.label1);
            this.Name = "CustomerChangeNameForm";
            this.Text = "Edit Name";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NewNameTextBox;
        private System.Windows.Forms.Button UpdateNameButton;
    }
}