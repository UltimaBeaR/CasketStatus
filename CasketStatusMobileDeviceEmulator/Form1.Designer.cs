namespace CasketStatusMobileDeviceEmulator
{
    partial class FormEmulator
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
            this.textBoxHostName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxResponse = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxNeedDescription = new System.Windows.Forms.CheckBox();
            this.buttonGetResponse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxHostName
            // 
            this.textBoxHostName.Location = new System.Drawing.Point(12, 30);
            this.textBoxHostName.Name = "textBoxHostName";
            this.textBoxHostName.Size = new System.Drawing.Size(448, 20);
            this.textBoxHostName.TabIndex = 0;
            this.textBoxHostName.Text = "http://localhost:55531";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "URL сайта Ларец.Статус (Например, http://localhost:55531)";
            // 
            // textBoxResponse
            // 
            this.textBoxResponse.Location = new System.Drawing.Point(12, 72);
            this.textBoxResponse.Multiline = true;
            this.textBoxResponse.Name = "textBoxResponse";
            this.textBoxResponse.Size = new System.Drawing.Size(448, 140);
            this.textBoxResponse.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ответ от сайта (JSON)";
            // 
            // checkBoxNeedDescription
            // 
            this.checkBoxNeedDescription.AutoSize = true;
            this.checkBoxNeedDescription.Location = new System.Drawing.Point(12, 218);
            this.checkBoxNeedDescription.Name = "checkBoxNeedDescription";
            this.checkBoxNeedDescription.Size = new System.Drawing.Size(179, 17);
            this.checkBoxNeedDescription.TabIndex = 4;
            this.checkBoxNeedDescription.Text = "Получать текстовое описание";
            this.checkBoxNeedDescription.UseVisualStyleBackColor = true;
            // 
            // buttonGetResponse
            // 
            this.buttonGetResponse.Location = new System.Drawing.Point(10, 252);
            this.buttonGetResponse.Name = "buttonGetResponse";
            this.buttonGetResponse.Size = new System.Drawing.Size(448, 44);
            this.buttonGetResponse.TabIndex = 5;
            this.buttonGetResponse.Text = "Получить ответ";
            this.buttonGetResponse.UseVisualStyleBackColor = true;
            this.buttonGetResponse.Click += new System.EventHandler(this.buttonGetResponse_Click);
            // 
            // FormEmulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 308);
            this.Controls.Add(this.buttonGetResponse);
            this.Controls.Add(this.checkBoxNeedDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxResponse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxHostName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormEmulator";
            this.Text = "Эмулятор мобильного устройства";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxHostName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxResponse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxNeedDescription;
        private System.Windows.Forms.Button buttonGetResponse;
    }
}

