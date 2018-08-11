<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fsl_rekapgaji
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btprev = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.cbpeg = New DevExpress.XtraEditors.LookUpEdit()
        Me.cbgolongan = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.cbbulan = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.ttahun = New DevExpress.XtraEditors.TextEdit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.cbpeg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbgolongan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbbulan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttahun.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btclose)
        Me.PanelControl1.Controls.Add(Me.btprev)
        Me.PanelControl1.Controls.Add(Me.LabelControl4)
        Me.PanelControl1.Controls.Add(Me.cbpeg)
        Me.PanelControl1.Controls.Add(Me.cbgolongan)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.cbbulan)
        Me.PanelControl1.Controls.Add(Me.LabelControl2)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Controls.Add(Me.ttahun)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(335, 169)
        Me.PanelControl1.TabIndex = 0
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(248, 134)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 23)
        Me.btclose.TabIndex = 5
        Me.btclose.Text = "&Close"
        '
        'btprev
        '
        Me.btprev.Location = New System.Drawing.Point(167, 134)
        Me.btprev.Name = "btprev"
        Me.btprev.Size = New System.Drawing.Size(75, 23)
        Me.btprev.TabIndex = 4
        Me.btprev.Text = "&Preview"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(38, 90)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(47, 13)
        Me.LabelControl4.TabIndex = 8
        Me.LabelControl4.Text = "Pegawai :"
        '
        'cbpeg
        '
        Me.cbpeg.Location = New System.Drawing.Point(91, 87)
        Me.cbpeg.Name = "cbpeg"
        Me.cbpeg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbpeg.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nip", 7, "NIP"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama", "Nama")})
        Me.cbpeg.Properties.DisplayMember = "nama"
        Me.cbpeg.Properties.NullText = ""
        Me.cbpeg.Properties.ValueMember = "nip"
        Me.cbpeg.Size = New System.Drawing.Size(195, 20)
        Me.cbpeg.TabIndex = 3
        '
        'cbgolongan
        '
        Me.cbgolongan.Location = New System.Drawing.Point(91, 61)
        Me.cbgolongan.Name = "cbgolongan"
        Me.cbgolongan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbgolongan.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kode", "Kd", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama", "Nama")})
        Me.cbgolongan.Properties.DisplayMember = "nama"
        Me.cbgolongan.Properties.NullText = ""
        Me.cbgolongan.Properties.ValueMember = "kode"
        Me.cbgolongan.Size = New System.Drawing.Size(195, 20)
        Me.cbgolongan.TabIndex = 2
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(33, 64)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(52, 13)
        Me.LabelControl3.TabIndex = 5
        Me.LabelControl3.Text = "Golongan :"
        '
        'cbbulan
        '
        Me.cbbulan.Location = New System.Drawing.Point(91, 35)
        Me.cbbulan.Name = "cbbulan"
        Me.cbbulan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbbulan.Properties.Items.AddRange(New Object() {"Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "Nopember", "Desember"})
        Me.cbbulan.Size = New System.Drawing.Size(195, 20)
        Me.cbbulan.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(52, 38)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(33, 13)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Bulan :"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(48, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(37, 13)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Tahun :"
        '
        'ttahun
        '
        Me.ttahun.Location = New System.Drawing.Point(91, 9)
        Me.ttahun.Name = "ttahun"
        Me.ttahun.Properties.DisplayFormat.FormatString = "d"
        Me.ttahun.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ttahun.Properties.EditFormat.FormatString = "d"
        Me.ttahun.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ttahun.Properties.Mask.EditMask = "d"
        Me.ttahun.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.ttahun.Size = New System.Drawing.Size(61, 20)
        Me.ttahun.TabIndex = 0
        '
        'fsl_rekapgaji
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(335, 169)
        Me.Controls.Add(Me.PanelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fsl_rekapgaji"
        Me.Text = "Rekap Gaji"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.cbpeg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbgolongan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbbulan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttahun.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttahun As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cbbulan As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbgolongan As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cbpeg As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btprev As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
End Class
