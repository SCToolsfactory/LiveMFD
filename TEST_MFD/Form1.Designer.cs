namespace TEST_MFD
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
    protected override void Dispose( bool disposing )
    {
      if ( disposing && ( components != null ) ) {
        components.Dispose( );
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.tlp = new System.Windows.Forms.TableLayoutPanel();
      this.MFD_L = new MFDlib.UC_MFDtest();
      this.MFD_R = new MFDlib.UC_MFDtest();
      this.pnlSignal = new System.Windows.Forms.Panel();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.tlp.SuspendLayout();
      this.SuspendLayout();
      // 
      // tlp
      // 
      this.tlp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(8)))));
      this.tlp.ColumnCount = 5;
      this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
      this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
      this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
      this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
      this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
      this.tlp.Controls.Add(this.MFD_L, 1, 1);
      this.tlp.Controls.Add(this.MFD_R, 3, 1);
      this.tlp.Controls.Add(this.pnlSignal, 2, 1);
      this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tlp.Location = new System.Drawing.Point(0, 0);
      this.tlp.Name = "tlp";
      this.tlp.RowCount = 3;
      this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
      this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 442F));
      this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tlp.Size = new System.Drawing.Size(1080, 561);
      this.tlp.TabIndex = 1;
      // 
      // MFD_L
      // 
      this.MFD_L.BackColor = System.Drawing.Color.Black;
      this.MFD_L.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.MFD_L.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MFD_L.Font = new System.Drawing.Font("Share Tech Mono", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MFD_L.Location = new System.Drawing.Point(43, 53);
      this.MFD_L.Margin = new System.Windows.Forms.Padding(0);
      this.MFD_L.Name = "MFD_L";
      this.MFD_L.Size = new System.Drawing.Size(432, 442);
      this.MFD_L.TabIndex = 0;
      // 
      // MFD_R
      // 
      this.MFD_R.BackColor = System.Drawing.Color.Black;
      this.MFD_R.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.MFD_R.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MFD_R.Font = new System.Drawing.Font("Share Tech Mono", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MFD_R.Location = new System.Drawing.Point(604, 53);
      this.MFD_R.Margin = new System.Windows.Forms.Padding(0);
      this.MFD_R.Name = "MFD_R";
      this.MFD_R.Size = new System.Drawing.Size(432, 442);
      this.MFD_R.TabIndex = 1;
      // 
      // pnlSignal
      // 
      this.pnlSignal.BackColor = System.Drawing.Color.Navy;
      this.pnlSignal.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlSignal.Location = new System.Drawing.Point(478, 56);
      this.pnlSignal.Name = "pnlSignal";
      this.pnlSignal.Size = new System.Drawing.Size(123, 436);
      this.pnlSignal.TabIndex = 2;
      // 
      // timer1
      // 
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(32)))));
      this.ClientSize = new System.Drawing.Size(1080, 561);
      this.Controls.Add(this.tlp);
      this.Font = new System.Drawing.Font("Corbel", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Form1";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
      this.Load += new System.EventHandler(this.Form1_Load);
      this.tlp.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.TableLayoutPanel tlp;
    private MFDlib.UC_MFDtest MFD_L;
    private MFDlib.UC_MFDtest MFD_R;
    private System.Windows.Forms.Panel pnlSignal;
    private System.Windows.Forms.Timer timer1;
  }
}

