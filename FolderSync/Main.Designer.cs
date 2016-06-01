namespace FolderSync
{
    partial class Main
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

            if (timeLeftTimer != null)
                timeLeftTimer.Dispose();
            if (timer != null)
                timer.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.source_txt = new System.Windows.Forms.TextBox();
            this.destination_txt = new System.Windows.Forms.TextBox();
            this.sync_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.interval_txt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.timeLeft_txt = new System.Windows.Forms.TextBox();
            this.overrideModes_cmb = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.stop_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // source_txt
            // 
            this.source_txt.Location = new System.Drawing.Point(186, 10);
            this.source_txt.Name = "source_txt";
            this.source_txt.Size = new System.Drawing.Size(875, 38);
            this.source_txt.TabIndex = 0;
            // 
            // destination_txt
            // 
            this.destination_txt.Location = new System.Drawing.Point(186, 57);
            this.destination_txt.Name = "destination_txt";
            this.destination_txt.Size = new System.Drawing.Size(875, 38);
            this.destination_txt.TabIndex = 1;
            // 
            // sync_btn
            // 
            this.sync_btn.Location = new System.Drawing.Point(864, 156);
            this.sync_btn.Name = "sync_btn";
            this.sync_btn.Size = new System.Drawing.Size(219, 92);
            this.sync_btn.TabIndex = 3;
            this.sync_btn.Text = "Sync Now / Start Sync";
            this.sync_btn.UseVisualStyleBackColor = true;
            this.sync_btn.Click += new System.EventHandler(this.sync_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 32);
            this.label1.TabIndex = 4;
            this.label1.Text = "Source:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 32);
            this.label2.TabIndex = 5;
            this.label2.Text = "Destination:";
            // 
            // interval_txt
            // 
            this.interval_txt.Location = new System.Drawing.Point(186, 102);
            this.interval_txt.Name = "interval_txt";
            this.interval_txt.Size = new System.Drawing.Size(875, 38);
            this.interval_txt.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 32);
            this.label3.TabIndex = 7;
            this.label3.Text = "Interval (S):";
            // 
            // timeLeft_txt
            // 
            this.timeLeft_txt.Location = new System.Drawing.Point(12, 270);
            this.timeLeft_txt.Name = "timeLeft_txt";
            this.timeLeft_txt.ReadOnly = true;
            this.timeLeft_txt.Size = new System.Drawing.Size(830, 38);
            this.timeLeft_txt.TabIndex = 8;
            // 
            // overrideModes_cmb
            // 
            this.overrideModes_cmb.FormattingEnabled = true;
            this.overrideModes_cmb.Items.AddRange(new object[] {
            "Never",
            "Always",
            "SourceNewer"});
            this.overrideModes_cmb.Location = new System.Drawing.Point(236, 156);
            this.overrideModes_cmb.Name = "overrideModes_cmb";
            this.overrideModes_cmb.Size = new System.Drawing.Size(606, 39);
            this.overrideModes_cmb.TabIndex = 9;
            this.overrideModes_cmb.Text = "Never";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(210, 32);
            this.label4.TabIndex = 10;
            this.label4.Text = "Override Mode:";
            // 
            // stop_btn
            // 
            this.stop_btn.Location = new System.Drawing.Point(864, 254);
            this.stop_btn.Name = "stop_btn";
            this.stop_btn.Size = new System.Drawing.Size(219, 69);
            this.stop_btn.TabIndex = 11;
            this.stop_btn.Text = "Stop";
            this.stop_btn.UseVisualStyleBackColor = true;
            this.stop_btn.Click += new System.EventHandler(this.stop_btn_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(240F, 240F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1095, 335);
            this.Controls.Add(this.stop_btn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.overrideModes_cmb);
            this.Controls.Add(this.timeLeft_txt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.interval_txt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sync_btn);
            this.Controls.Add(this.destination_txt);
            this.Controls.Add(this.source_txt);
            this.Name = "Main";
            this.Text = "FolderSync";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox source_txt;
        private System.Windows.Forms.TextBox destination_txt;
        private System.Windows.Forms.Button sync_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox interval_txt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox timeLeft_txt;
        private System.Windows.Forms.ComboBox overrideModes_cmb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button stop_btn;
    }
}

