namespace Generator {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose( );
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( ) {
            this.lblMapSeed = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbRoom = new System.Windows.Forms.PictureBox();
            this.tbMapSeed = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblGenMapSeed = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbWay = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRoom)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMapSeed
            // 
            this.lblMapSeed.AutoSize = true;
            this.lblMapSeed.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblMapSeed.ForeColor = System.Drawing.Color.White;
            this.lblMapSeed.Location = new System.Drawing.Point(12, 16);
            this.lblMapSeed.Name = "lblMapSeed";
            this.lblMapSeed.Size = new System.Drawing.Size(60, 26);
            this.lblMapSeed.TabIndex = 0;
            this.lblMapSeed.Text = "Seed";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnGenerate.Location = new System.Drawing.Point(258, 12);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(148, 32);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbWay);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblGenMapSeed);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pbRoom);
            this.groupBox1.Font = new System.Drawing.Font("Consolas", 16F);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(647, 411);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Map";
            // 
            // pbRoom
            // 
            this.pbRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.pbRoom.Location = new System.Drawing.Point(6, 57);
            this.pbRoom.Name = "pbRoom";
            this.pbRoom.Size = new System.Drawing.Size(256, 128);
            this.pbRoom.TabIndex = 0;
            this.pbRoom.TabStop = false;
            // 
            // tbMapSeed
            // 
            this.tbMapSeed.BackColor = System.Drawing.Color.DimGray;
            this.tbMapSeed.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbMapSeed.ForeColor = System.Drawing.Color.White;
            this.tbMapSeed.Location = new System.Drawing.Point(92, 13);
            this.tbMapSeed.MaxLength = 11;
            this.tbMapSeed.Name = "tbMapSeed";
            this.tbMapSeed.Size = new System.Drawing.Size(148, 32);
            this.tbMapSeed.TabIndex = 1;
            this.tbMapSeed.Text = "0";
            this.tbMapSeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbMapSeed.TextChanged += new System.EventHandler(this.tbMapSeed_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "Seed";
            // 
            // lblGenMapSeed
            // 
            this.lblGenMapSeed.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblGenMapSeed.ForeColor = System.Drawing.Color.White;
            this.lblGenMapSeed.Location = new System.Drawing.Point(75, 28);
            this.lblGenMapSeed.Name = "lblGenMapSeed";
            this.lblGenMapSeed.Size = new System.Drawing.Size(153, 26);
            this.lblGenMapSeed.TabIndex = 5;
            this.lblGenMapSeed.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(280, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 26);
            this.label2.TabIndex = 6;
            this.label2.Text = "Way";
            // 
            // lbWay
            // 
            this.lbWay.Font = new System.Drawing.Font("Consolas", 8F);
            this.lbWay.FormattingEnabled = true;
            this.lbWay.Location = new System.Drawing.Point(285, 57);
            this.lbWay.Name = "lbWay";
            this.lbWay.Size = new System.Drawing.Size(356, 121);
            this.lbWay.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(1008, 473);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.tbMapSeed);
            this.Controls.Add(this.lblMapSeed);
            this.Name = "Form1";
            this.Text = "Chamber Generator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMapSeed;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pbRoom;
        private System.Windows.Forms.Label lblGenMapSeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbMapSeed;
        private System.Windows.Forms.ListBox lbWay;
        private System.Windows.Forms.Label label2;
    }
}

