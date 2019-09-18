namespace NepTrans
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbRootDirectory = new System.Windows.Forms.TextBox();
            this.dgvWorkspace = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTextEng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTextJap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTextVie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkUseCurrentDir = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupTextEng = new System.Windows.Forms.GroupBox();
            this.tbTextEng = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbTextJap = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbTextVie = new System.Windows.Forms.TextBox();
            this.treeDirStruct = new System.Windows.Forms.TreeView();
            this.btnSelectRootDir = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSaveEntry = new System.Windows.Forms.Button();
            this.pbOverall = new System.Windows.Forms.ProgressBar();
            this.pbGameScript = new System.Windows.Forms.ProgressBar();
            this.pbSystemScript = new System.Windows.Forms.ProgressBar();
            this.pbCurEntryStat = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCurEntryStat = new System.Windows.Forms.Label();
            this.lblOverallStat = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblGameScriptStat = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSystemScriptStat = new System.Windows.Forms.Label();
            this.lblSystemScriptHeader = new System.Windows.Forms.Label();
            this.btnVerifyDirectory = new System.Windows.Forms.Button();
            this.btSaveProj = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkspace)).BeginInit();
            this.groupTextEng.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbRootDirectory
            // 
            this.tbRootDirectory.Location = new System.Drawing.Point(93, 12);
            this.tbRootDirectory.Name = "tbRootDirectory";
            this.tbRootDirectory.Size = new System.Drawing.Size(875, 20);
            this.tbRootDirectory.TabIndex = 0;
            // 
            // dgvWorkspace
            // 
            this.dgvWorkspace.AllowUserToAddRows = false;
            this.dgvWorkspace.AllowUserToDeleteRows = false;
            this.dgvWorkspace.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colTextEng,
            this.colTextJap,
            this.colTextVie});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvWorkspace.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvWorkspace.Location = new System.Drawing.Point(268, 67);
            this.dgvWorkspace.MultiSelect = false;
            this.dgvWorkspace.Name = "dgvWorkspace";
            this.dgvWorkspace.ReadOnly = true;
            this.dgvWorkspace.RowHeadersWidth = 25;
            this.dgvWorkspace.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWorkspace.Size = new System.Drawing.Size(876, 260);
            this.dgvWorkspace.TabIndex = 1;
            this.dgvWorkspace.SelectionChanged += new System.EventHandler(this.dgvWorkspace_SelectionChanged);
            // 
            // colId
            // 
            this.colId.Frozen = true;
            this.colId.HeaderText = "id";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colId.Width = 75;
            // 
            // colTextEng
            // 
            this.colTextEng.Frozen = true;
            this.colTextEng.HeaderText = "eng";
            this.colTextEng.Name = "colTextEng";
            this.colTextEng.ReadOnly = true;
            this.colTextEng.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colTextEng.Width = 250;
            // 
            // colTextJap
            // 
            this.colTextJap.Frozen = true;
            this.colTextJap.HeaderText = "jap";
            this.colTextJap.Name = "colTextJap";
            this.colTextJap.ReadOnly = true;
            this.colTextJap.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colTextJap.Width = 250;
            // 
            // colTextVie
            // 
            this.colTextVie.Frozen = true;
            this.colTextVie.HeaderText = "vie";
            this.colTextVie.Name = "colTextVie";
            this.colTextVie.ReadOnly = true;
            this.colTextVie.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colTextVie.Width = 250;
            // 
            // checkUseCurrentDir
            // 
            this.checkUseCurrentDir.AutoSize = true;
            this.checkUseCurrentDir.Enabled = false;
            this.checkUseCurrentDir.Location = new System.Drawing.Point(248, 42);
            this.checkUseCurrentDir.Name = "checkUseCurrentDir";
            this.checkUseCurrentDir.Size = new System.Drawing.Size(127, 17);
            this.checkUseCurrentDir.TabIndex = 2;
            this.checkUseCurrentDir.Text = "Use Current Directory";
            this.checkUseCurrentDir.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Root Directory";
            // 
            // groupTextEng
            // 
            this.groupTextEng.Controls.Add(this.tbTextEng);
            this.groupTextEng.Location = new System.Drawing.Point(268, 333);
            this.groupTextEng.Name = "groupTextEng";
            this.groupTextEng.Size = new System.Drawing.Size(325, 100);
            this.groupTextEng.TabIndex = 6;
            this.groupTextEng.TabStop = false;
            this.groupTextEng.Text = "English Text";
            // 
            // tbTextEng
            // 
            this.tbTextEng.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTextEng.Location = new System.Drawing.Point(6, 19);
            this.tbTextEng.Multiline = true;
            this.tbTextEng.Name = "tbTextEng";
            this.tbTextEng.ReadOnly = true;
            this.tbTextEng.Size = new System.Drawing.Size(313, 75);
            this.tbTextEng.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbTextJap);
            this.groupBox1.Location = new System.Drawing.Point(599, 333);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 100);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Japanese Text";
            // 
            // tbTextJap
            // 
            this.tbTextJap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTextJap.Location = new System.Drawing.Point(6, 19);
            this.tbTextJap.Multiline = true;
            this.tbTextJap.Name = "tbTextJap";
            this.tbTextJap.ReadOnly = true;
            this.tbTextJap.Size = new System.Drawing.Size(313, 75);
            this.tbTextJap.TabIndex = 2;
            this.tbTextJap.Text = "冗談はその下品な言葉使いだけにしてくださる？\r\nわたくしは成し遂げなければならない崇高なる\r\n目的の為…ここで敗れるわけにはいきませんの";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbTextVie);
            this.groupBox2.Location = new System.Drawing.Point(268, 439);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(325, 100);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Translated Text";
            // 
            // tbTextVie
            // 
            this.tbTextVie.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTextVie.Location = new System.Drawing.Point(6, 19);
            this.tbTextVie.Multiline = true;
            this.tbTextVie.Name = "tbTextVie";
            this.tbTextVie.Size = new System.Drawing.Size(313, 75);
            this.tbTextVie.TabIndex = 0;
            this.tbTextVie.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTextVie_KeyDown);
            // 
            // treeDirStruct
            // 
            this.treeDirStruct.HideSelection = false;
            this.treeDirStruct.HotTracking = true;
            this.treeDirStruct.Location = new System.Drawing.Point(12, 67);
            this.treeDirStruct.Name = "treeDirStruct";
            this.treeDirStruct.Size = new System.Drawing.Size(250, 602);
            this.treeDirStruct.TabIndex = 9;
            this.treeDirStruct.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeDirStruct_NodeMouseClick);
            // 
            // btnSelectRootDir
            // 
            this.btnSelectRootDir.Location = new System.Drawing.Point(92, 38);
            this.btnSelectRootDir.Name = "btnSelectRootDir";
            this.btnSelectRootDir.Size = new System.Drawing.Size(150, 23);
            this.btnSelectRootDir.TabIndex = 10;
            this.btnSelectRootDir.Text = "Select Directory";
            this.btnSelectRootDir.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(599, 510);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(144, 23);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "Apply This Record";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSaveEntry
            // 
            this.btnSaveEntry.Location = new System.Drawing.Point(930, 404);
            this.btnSaveEntry.Name = "btnSaveEntry";
            this.btnSaveEntry.Size = new System.Drawing.Size(214, 23);
            this.btnSaveEntry.TabIndex = 12;
            this.btnSaveEntry.Text = "Save Current Entry";
            this.btnSaveEntry.UseVisualStyleBackColor = true;
            this.btnSaveEntry.Click += new System.EventHandler(this.btnSaveEntry_Click);
            // 
            // pbOverall
            // 
            this.pbOverall.Location = new System.Drawing.Point(268, 545);
            this.pbOverall.Name = "pbOverall";
            this.pbOverall.Size = new System.Drawing.Size(876, 15);
            this.pbOverall.TabIndex = 13;
            // 
            // pbGameScript
            // 
            this.pbGameScript.Location = new System.Drawing.Point(268, 579);
            this.pbGameScript.MinimumSize = new System.Drawing.Size(131, 15);
            this.pbGameScript.Name = "pbGameScript";
            this.pbGameScript.Size = new System.Drawing.Size(435, 15);
            this.pbGameScript.TabIndex = 14;
            // 
            // pbSystemScript
            // 
            this.pbSystemScript.Location = new System.Drawing.Point(709, 579);
            this.pbSystemScript.MinimumSize = new System.Drawing.Size(132, 15);
            this.pbSystemScript.Name = "pbSystemScript";
            this.pbSystemScript.Size = new System.Drawing.Size(435, 15);
            this.pbSystemScript.TabIndex = 15;
            // 
            // pbCurEntryStat
            // 
            this.pbCurEntryStat.Location = new System.Drawing.Point(930, 352);
            this.pbCurEntryStat.Name = "pbCurEntryStat";
            this.pbCurEntryStat.Size = new System.Drawing.Size(214, 15);
            this.pbCurEntryStat.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(930, 370);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Current Entry:";
            // 
            // lblCurEntryStat
            // 
            this.lblCurEntryStat.AutoSize = true;
            this.lblCurEntryStat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurEntryStat.Location = new System.Drawing.Point(1007, 370);
            this.lblCurEntryStat.Name = "lblCurEntryStat";
            this.lblCurEntryStat.Size = new System.Drawing.Size(55, 13);
            this.lblCurEntryStat.TabIndex = 19;
            this.lblCurEntryStat.Text = "0/0 (0%)";
            // 
            // lblOverallStat
            // 
            this.lblOverallStat.AutoSize = true;
            this.lblOverallStat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverallStat.Location = new System.Drawing.Point(361, 563);
            this.lblOverallStat.Name = "lblOverallStat";
            this.lblOverallStat.Size = new System.Drawing.Size(55, 13);
            this.lblOverallStat.TabIndex = 21;
            this.lblOverallStat.Text = "0/0 (0%)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(268, 563);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Overall Progress:";
            // 
            // lblGameScriptStat
            // 
            this.lblGameScriptStat.AutoSize = true;
            this.lblGameScriptStat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameScriptStat.Location = new System.Drawing.Point(344, 597);
            this.lblGameScriptStat.Name = "lblGameScriptStat";
            this.lblGameScriptStat.Size = new System.Drawing.Size(55, 13);
            this.lblGameScriptStat.TabIndex = 23;
            this.lblGameScriptStat.Text = "0/0 (0%)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(268, 597);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Dialog Script:";
            // 
            // lblSystemScriptStat
            // 
            this.lblSystemScriptStat.AutoSize = true;
            this.lblSystemScriptStat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSystemScriptStat.Location = new System.Drawing.Point(789, 597);
            this.lblSystemScriptStat.Name = "lblSystemScriptStat";
            this.lblSystemScriptStat.Size = new System.Drawing.Size(55, 13);
            this.lblSystemScriptStat.TabIndex = 25;
            this.lblSystemScriptStat.Text = "0/0 (0%)";
            // 
            // lblSystemScriptHeader
            // 
            this.lblSystemScriptHeader.AutoSize = true;
            this.lblSystemScriptHeader.Location = new System.Drawing.Point(709, 597);
            this.lblSystemScriptHeader.Name = "lblSystemScriptHeader";
            this.lblSystemScriptHeader.Size = new System.Drawing.Size(74, 13);
            this.lblSystemScriptHeader.TabIndex = 24;
            this.lblSystemScriptHeader.Text = "System Script:";
            // 
            // btnVerifyDirectory
            // 
            this.btnVerifyDirectory.Enabled = false;
            this.btnVerifyDirectory.Location = new System.Drawing.Point(974, 10);
            this.btnVerifyDirectory.Name = "btnVerifyDirectory";
            this.btnVerifyDirectory.Size = new System.Drawing.Size(150, 23);
            this.btnVerifyDirectory.TabIndex = 26;
            this.btnVerifyDirectory.Text = "Verify Directory Structure";
            this.btnVerifyDirectory.UseVisualStyleBackColor = true;
            // 
            // btSaveProj
            // 
            this.btSaveProj.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSaveProj.ForeColor = System.Drawing.Color.Red;
            this.btSaveProj.Location = new System.Drawing.Point(930, 510);
            this.btSaveProj.Name = "btSaveProj";
            this.btSaveProj.Size = new System.Drawing.Size(214, 23);
            this.btSaveProj.TabIndex = 27;
            this.btSaveProj.Text = "Save Project";
            this.btSaveProj.UseVisualStyleBackColor = true;
            this.btSaveProj.Click += new System.EventHandler(this.btSaveProj_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.btSaveProj);
            this.Controls.Add(this.btnVerifyDirectory);
            this.Controls.Add(this.lblSystemScriptStat);
            this.Controls.Add(this.lblSystemScriptHeader);
            this.Controls.Add(this.lblGameScriptStat);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblOverallStat);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblCurEntryStat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pbCurEntryStat);
            this.Controls.Add(this.pbSystemScript);
            this.Controls.Add(this.pbGameScript);
            this.Controls.Add(this.pbOverall);
            this.Controls.Add(this.btnSaveEntry);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnSelectRootDir);
            this.Controls.Add(this.treeDirStruct);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupTextEng);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkUseCurrentDir);
            this.Controls.Add(this.dgvWorkspace);
            this.Controls.Add(this.tbRootDirectory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkspace)).EndInit();
            this.groupTextEng.ResumeLayout(false);
            this.groupTextEng.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbRootDirectory;
        private System.Windows.Forms.DataGridView dgvWorkspace;
        private System.Windows.Forms.CheckBox checkUseCurrentDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupTextEng;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbTextVie;
        private System.Windows.Forms.TreeView treeDirStruct;
        private System.Windows.Forms.Button btnSelectRootDir;
        private System.Windows.Forms.TextBox tbTextEng;
        private System.Windows.Forms.TextBox tbTextJap;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnSaveEntry;
        private System.Windows.Forms.ProgressBar pbOverall;
        private System.Windows.Forms.ProgressBar pbGameScript;
        private System.Windows.Forms.ProgressBar pbSystemScript;
        private System.Windows.Forms.ProgressBar pbCurEntryStat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCurEntryStat;
        private System.Windows.Forms.Label lblOverallStat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblGameScriptStat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblSystemScriptStat;
        private System.Windows.Forms.Label lblSystemScriptHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTextEng;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTextJap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTextVie;
        private System.Windows.Forms.Button btnVerifyDirectory;
        private System.Windows.Forms.Button btSaveProj;
    }
}

