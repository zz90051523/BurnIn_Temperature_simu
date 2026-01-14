namespace BurnIn_Temperature_simu
{
    partial class UpdateForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlColors = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtChangelog = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnRemindLater = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48))))); // #2D2D30
            this.ClientSize = new System.Drawing.Size(500, 350);
            this.Controls.Add(this.btnRemindLater);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtChangelog);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlColors);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UpdateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Software Update";
            // 
            // pnlColors (Top Bar)
            // 
            this.pnlColors.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.pnlColors.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlColors.Location = new System.Drawing.Point(0, 0);
            this.pnlColors.Name = "pnlColors";
            this.pnlColors.Size = new System.Drawing.Size(500, 4);
            this.pnlColors.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Silver;
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(126, 19);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Software Update";
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseDown);
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(20, 55);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(239, 26);
            this.lblHeader.TabIndex = 2;
            this.lblHeader.Text = "A new version is available!";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69))))); // Success Green
            this.lblVersion.Location = new System.Drawing.Point(22, 90);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(63, 19);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "v1.0.X";
            // 
            // txtChangelog
            // 
            this.txtChangelog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtChangelog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtChangelog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtChangelog.ForeColor = System.Drawing.Color.Gray;
            this.txtChangelog.Location = new System.Drawing.Point(24, 130);
            this.txtChangelog.Multiline = true;
            this.txtChangelog.Name = "txtChangelog";
            this.txtChangelog.ReadOnly = true;
            this.txtChangelog.Size = new System.Drawing.Size(450, 140);
            this.txtChangelog.TabIndex = 4;
            this.txtChangelog.Text = "Loading release notes...";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69))))); // Green
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(324, 290);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(150, 40);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Update Now";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnRemindLater
            // 
            this.btnRemindLater.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60))))); // Dark Gray
            this.btnRemindLater.FlatAppearance.BorderSize = 0;
            this.btnRemindLater.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemindLater.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btnRemindLater.ForeColor = System.Drawing.Color.Silver;
            this.btnRemindLater.Location = new System.Drawing.Point(190, 295);
            this.btnRemindLater.Name = "btnRemindLater";
            this.btnRemindLater.Size = new System.Drawing.Size(120, 30);
            this.btnRemindLater.TabIndex = 6;
            this.btnRemindLater.Text = "Remind Me Later";
            this.btnRemindLater.UseVisualStyleBackColor = false;
            this.btnRemindLater.Click += new System.EventHandler(this.btnRemindLater_Click);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel pnlColors;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TextBox txtChangelog;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnRemindLater;
    }
}
