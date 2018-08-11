<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fslrekap_absen
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
        Me.ttgl2 = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl1 = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.cbpeg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbgolongan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl1.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.PanelControl1.Controls.Add(Me.ttgl2)
        Me.PanelControl1.Controls.Add(Me.LabelControl2)
        Me.PanelControl1.Controls.Add(Me.ttgl1)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(342, 169)
        Me.PanelControl1.TabIndex = 1
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(244, 128)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 23)
        Me.btclose.TabIndex = 5
        Me.btclose.Text = "&Close"
        '
        'btprev
        '
        Me.btprev.Location = New System.Drawing.Point(163, 128)
        Me.btprev.Name = "btprev"
        Me.btprev.Size = New System.Drawing.Size(75, 23)
        Me.btprev.TabIndex = 4
        Me.btprev.Text = "&Preview"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(22, 77)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(47, 13)
        Me.LabelControl4.TabIndex = 9
        Me.LabelControl4.Text = "Pegawai :"
        '
        'cbpeg
        '
        Me.cbpeg.Location = New System.Drawing.Point(75, 74)
        Me.cbpeg.Name = "cbpeg"
        Me.cbpeg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbpeg.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nip", "Kd", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama", "Nama")})
        Me.cbpeg.Properties.DisplayMember = "nama"
        Me.cbpeg.Properties.NullText = ""
        Me.cbpeg.Properties.ValueMember = "nip"
        Me.cbpeg.Size = New System.Drawing.Size(227, 20)
        Me.cbpeg.TabIndex = 3
        '
        'cbgolongan
        '
        Me.cbgolongan.Location = New System.Drawing.Point(75, 48)
        Me.cbgolongan.Name = "cbgolongan"
        Me.cbgolongan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbgolongan.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kode", "Kd", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama", "Nama")})
        Me.cbgolongan.Properties.DisplayMember = "nama"
        Me.cbgolongan.Properties.NullText = ""
        Me.cbgolongan.Properties.ValueMember = "kode"
        Me.cbgolongan.Size = New System.Drawing.Size(227, 20)
        Me.cbgolongan.TabIndex = 2
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(17, 51)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(52, 13)
        Me.LabelControl3.TabIndex = 4
        Me.LabelControl3.Text = "Golongan :"
        '
        'ttgl2
        '
        Me.ttgl2.EditValue = Nothing
        Me.ttgl2.Location = New System.Drawing.Point(202, 22)
        Me.ttgl2.Name = "ttgl2"
        Me.ttgl2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl2.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl2.Size = New System.Drawing.Size(100, 20)
        Me.ttgl2.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(181, 25)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(15, 13)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "s.d"
        '
        'ttgl1
        '
        Me.ttgl1.EditValue = Nothing
        Me.ttgl1.Location = New System.Drawing.Point(75, 22)
        Me.ttgl1.Name = "ttgl1"
        Me.ttgl1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl1.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl1.Size = New System.Drawing.Size(100, 20)
        Me.ttgl1.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(24, 25)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(45, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Tanggal :"
        '
        'fslrekap_absen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(342, 169)
        Me.Controls.Add(Me.PanelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fslrekap_absen"
        Me.Text = "Rekap Absensi"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.cbpeg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbgolongan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl1.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btprev As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbpeg As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cbgolongan As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl2 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
