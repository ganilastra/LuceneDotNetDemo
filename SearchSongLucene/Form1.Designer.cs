﻿namespace SearchSongLucene
{
    partial class SearchSongForm
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
            this.gvResults = new System.Windows.Forms.DataGridView();
            this.ddlLanguage = new System.Windows.Forms.ComboBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.dtReleaseFrom = new System.Windows.Forms.DateTimePicker();
            this.lblDtFrom = new System.Windows.Forms.Label();
            this.dtReleaseTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnIndexData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // gvResults
            // 
            this.gvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvResults.Location = new System.Drawing.Point(26, 12);
            this.gvResults.Name = "gvResults";
            this.gvResults.RowHeadersWidth = 51;
            this.gvResults.RowTemplate.Height = 24;
            this.gvResults.Size = new System.Drawing.Size(1221, 369);
            this.gvResults.TabIndex = 0;
            // 
            // ddlLanguage
            // 
            this.ddlLanguage.FormattingEnabled = true;
            this.ddlLanguage.IntegralHeight = false;
            this.ddlLanguage.Items.AddRange(new object[] {
            "English",
            "Spanish",
            "Filipino"});
            this.ddlLanguage.Location = new System.Drawing.Point(384, 503);
            this.ddlLanguage.Name = "ddlLanguage";
            this.ddlLanguage.Size = new System.Drawing.Size(225, 24);
            this.ddlLanguage.TabIndex = 1;
            this.ddlLanguage.SelectedIndexChanged += new System.EventHandler(this.ddlLanguage_SelectedIndexChanged);
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLanguage.Location = new System.Drawing.Point(234, 499);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(121, 25);
            this.lblLanguage.TabIndex = 2;
            this.lblLanguage.Text = "Language :";
            // 
            // dtReleaseFrom
            // 
            this.dtReleaseFrom.Location = new System.Drawing.Point(384, 567);
            this.dtReleaseFrom.Name = "dtReleaseFrom";
            this.dtReleaseFrom.Size = new System.Drawing.Size(241, 22);
            this.dtReleaseFrom.TabIndex = 3;
            // 
            // lblDtFrom
            // 
            this.lblDtFrom.AutoSize = true;
            this.lblDtFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDtFrom.Location = new System.Drawing.Point(197, 564);
            this.lblDtFrom.Name = "lblDtFrom";
            this.lblDtFrom.Size = new System.Drawing.Size(158, 25);
            this.lblDtFrom.TabIndex = 4;
            this.lblDtFrom.Text = "Release From :";
            this.lblDtFrom.Click += new System.EventHandler(this.label1_Click);
            // 
            // dtReleaseTo
            // 
            this.dtReleaseTo.Location = new System.Drawing.Point(713, 566);
            this.dtReleaseTo.Name = "dtReleaseTo";
            this.dtReleaseTo.Size = new System.Drawing.Size(200, 22);
            this.dtReleaseTo.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(642, 564);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "To :";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(384, 437);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(489, 22);
            this.txtSearch.TabIndex = 9;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(261, 433);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(94, 25);
            this.lblSearch.TabIndex = 10;
            this.lblSearch.Text = "Search :";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1011, 406);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(152, 118);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnIndexData
            // 
            this.btnIndexData.Location = new System.Drawing.Point(37, 420);
            this.btnIndexData.Name = "btnIndexData";
            this.btnIndexData.Size = new System.Drawing.Size(144, 118);
            this.btnIndexData.TabIndex = 13;
            this.btnIndexData.Text = "Index Data";
            this.btnIndexData.UseVisualStyleBackColor = true;
            this.btnIndexData.Click += new System.EventHandler(this.btnIndexData_Click);
            // 
            // SearchSongForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1540, 759);
            this.Controls.Add(this.btnIndexData);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtReleaseTo);
            this.Controls.Add(this.lblDtFrom);
            this.Controls.Add(this.dtReleaseFrom);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.ddlLanguage);
            this.Controls.Add(this.gvResults);
            this.Name = "SearchSongForm";
            this.Text = "Search Song";
            this.Load += new System.EventHandler(this.SearchSongForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvResults;
        private System.Windows.Forms.ComboBox ddlLanguage;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.DateTimePicker dtReleaseFrom;
        private System.Windows.Forms.Label lblDtFrom;
        private System.Windows.Forms.DateTimePicker dtReleaseTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnIndexData;
    }
}

