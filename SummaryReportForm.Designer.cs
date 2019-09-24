namespace NepTrans
{
    partial class SummaryReportForm
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
            this.pnlReport = new System.Windows.Forms.Panel();
            this.lblGameScriptHead = new System.Windows.Forms.Label();
            this.lblSystemScriptHead = new System.Windows.Forms.Label();
            this.lblOveralHead = new System.Windows.Forms.Label();
            this.lblOverall = new System.Windows.Forms.Label();
            this.lblSystemScript = new System.Windows.Forms.Label();
            this.lblGameScript = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnlReport
            // 
            this.pnlReport.Location = new System.Drawing.Point(0, 0);
            this.pnlReport.Name = "pnlReport";
            this.pnlReport.Size = new System.Drawing.Size(281, 281);
            this.pnlReport.TabIndex = 0;
            this.pnlReport.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlReport_Paint);
            // 
            // lblGameScriptHead
            // 
            this.lblGameScriptHead.AutoSize = true;
            this.lblGameScriptHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameScriptHead.Location = new System.Drawing.Point(287, 89);
            this.lblGameScriptHead.Name = "lblGameScriptHead";
            this.lblGameScriptHead.Size = new System.Drawing.Size(70, 13);
            this.lblGameScriptHead.TabIndex = 1;
            this.lblGameScriptHead.Text = "Dialog Script:";
            // 
            // lblSystemScriptHead
            // 
            this.lblSystemScriptHead.AutoSize = true;
            this.lblSystemScriptHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSystemScriptHead.Location = new System.Drawing.Point(287, 120);
            this.lblSystemScriptHead.Name = "lblSystemScriptHead";
            this.lblSystemScriptHead.Size = new System.Drawing.Size(74, 13);
            this.lblSystemScriptHead.TabIndex = 2;
            this.lblSystemScriptHead.Text = "System Script:";
            // 
            // lblOveralHead
            // 
            this.lblOveralHead.AutoSize = true;
            this.lblOveralHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOveralHead.Location = new System.Drawing.Point(287, 166);
            this.lblOveralHead.Name = "lblOveralHead";
            this.lblOveralHead.Size = new System.Drawing.Size(43, 13);
            this.lblOveralHead.TabIndex = 3;
            this.lblOveralHead.Text = "Overall:";
            // 
            // lblOverall
            // 
            this.lblOverall.AutoSize = true;
            this.lblOverall.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverall.Location = new System.Drawing.Point(307, 179);
            this.lblOverall.Name = "lblOverall";
            this.lblOverall.Size = new System.Drawing.Size(55, 13);
            this.lblOverall.TabIndex = 6;
            this.lblOverall.Text = "0/0 (0%)";
            // 
            // lblSystemScript
            // 
            this.lblSystemScript.AutoSize = true;
            this.lblSystemScript.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSystemScript.Location = new System.Drawing.Point(307, 133);
            this.lblSystemScript.Name = "lblSystemScript";
            this.lblSystemScript.Size = new System.Drawing.Size(55, 13);
            this.lblSystemScript.TabIndex = 5;
            this.lblSystemScript.Text = "0/0 (0%)";
            // 
            // lblGameScript
            // 
            this.lblGameScript.AutoSize = true;
            this.lblGameScript.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameScript.Location = new System.Drawing.Point(307, 102);
            this.lblGameScript.Name = "lblGameScript";
            this.lblGameScript.Size = new System.Drawing.Size(55, 13);
            this.lblGameScript.TabIndex = 4;
            this.lblGameScript.Text = "0/0 (0%)";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(413, 246);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SummaryReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(500, 281);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblOverall);
            this.Controls.Add(this.lblSystemScript);
            this.Controls.Add(this.lblGameScript);
            this.Controls.Add(this.lblOveralHead);
            this.Controls.Add(this.lblSystemScriptHead);
            this.Controls.Add(this.lblGameScriptHead);
            this.Controls.Add(this.pnlReport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SummaryReportForm";
            this.Text = "Progress Summary";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlReport;
        private System.Windows.Forms.Label lblGameScriptHead;
        private System.Windows.Forms.Label lblSystemScriptHead;
        private System.Windows.Forms.Label lblOveralHead;
        private System.Windows.Forms.Label lblOverall;
        private System.Windows.Forms.Label lblSystemScript;
        private System.Windows.Forms.Label lblGameScript;
        private System.Windows.Forms.Button btnClose;
    }
}