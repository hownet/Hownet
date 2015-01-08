namespace Hownet.BaseForm
{
    partial class frSpecOne
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this._teName = new DevExpress.XtraEditors.TextEdit();
            this._sbSaveAndContinue = new DevExpress.XtraEditors.SimpleButton();
            this._sbSaveAndExit = new DevExpress.XtraEditors.SimpleButton();
            this._sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this._teOrders = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this._teName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._teOrders.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 11);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "规格名称：";
            // 
            // _teName
            // 
            this._teName.Location = new System.Drawing.Point(69, 8);
            this._teName.Name = "_teName";
            this._teName.Size = new System.Drawing.Size(219, 20);
            this._teName.TabIndex = 7;
            // 
            // _sbSaveAndContinue
            // 
            this._sbSaveAndContinue.Location = new System.Drawing.Point(18, 71);
            this._sbSaveAndContinue.Name = "_sbSaveAndContinue";
            this._sbSaveAndContinue.Size = new System.Drawing.Size(75, 23);
            this._sbSaveAndContinue.TabIndex = 10;
            this._sbSaveAndContinue.Text = "&s保存并继续";
            this._sbSaveAndContinue.Click += new System.EventHandler(this._sbSaveAndContinue_Click);
            // 
            // _sbSaveAndExit
            // 
            this._sbSaveAndExit.Location = new System.Drawing.Point(114, 70);
            this._sbSaveAndExit.Name = "_sbSaveAndExit";
            this._sbSaveAndExit.Size = new System.Drawing.Size(75, 23);
            this._sbSaveAndExit.TabIndex = 11;
            this._sbSaveAndExit.Text = "&x保存后退出";
            this._sbSaveAndExit.Click += new System.EventHandler(this._sbSaveAndExit_Click);
            // 
            // _sbCancel
            // 
            this._sbCancel.Location = new System.Drawing.Point(210, 69);
            this._sbCancel.Name = "_sbCancel";
            this._sbCancel.Size = new System.Drawing.Size(75, 23);
            this._sbCancel.TabIndex = 12;
            this._sbCancel.Text = "&c取消";
            this._sbCancel.Click += new System.EventHandler(this._sbCancel_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 37);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "规格说明：";
            // 
            // _teOrders
            // 
            this._teOrders.Location = new System.Drawing.Point(69, 34);
            this._teOrders.Name = "_teOrders";
            this._teOrders.Size = new System.Drawing.Size(219, 20);
            this._teOrders.TabIndex = 7;
            // 
            // frSpecOne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(300, 103);
            this.Controls.Add(this._sbCancel);
            this.Controls.Add(this._sbSaveAndExit);
            this.Controls.Add(this._sbSaveAndContinue);
            this.Controls.Add(this._teOrders);
            this.Controls.Add(this._teName);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frSpecOne";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frMeasureOne";
            this.Load += new System.EventHandler(this.frColorOne_Load);
            ((System.ComponentModel.ISupportInitialize)(this._teName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._teOrders.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit _teName;
        private DevExpress.XtraEditors.SimpleButton _sbSaveAndContinue;
        private DevExpress.XtraEditors.SimpleButton _sbSaveAndExit;
        private DevExpress.XtraEditors.SimpleButton _sbCancel;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit _teOrders;
    }
}