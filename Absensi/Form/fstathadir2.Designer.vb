<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fstathadir2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fstathadir2))
        Me.v = New DevExpress.XtraEditors.LabelControl()
        Me.tnip = New DevExpress.XtraEditors.TextEdit()
        Me.btcnip = New DevExpress.XtraEditors.SimpleButton()
        Me.tnama = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl1 = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tjam1 = New DevExpress.XtraEditors.TimeEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.tjam2 = New DevExpress.XtraEditors.TimeEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl2 = New DevExpress.XtraEditors.DateEdit()
        Me.tcbostat = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.tket = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        Me.btselesai = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.cstat = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.tnip.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tjam1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tjam2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tcbostat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tket.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cstat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'v
        '
        Me.v.Location = New System.Drawing.Point(55, 15)
        Me.v.Name = "v"
        Me.v.Size = New System.Drawing.Size(22, 13)
        Me.v.TabIndex = 0
        Me.v.Text = "Nip :"
        '
        'tnip
        '
        Me.tnip.Location = New System.Drawing.Point(83, 12)
        Me.tnip.Name = "tnip"
        Me.tnip.Size = New System.Drawing.Size(100, 20)
        Me.tnip.TabIndex = 0
        '
        'btcnip
        '
        Me.btcnip.Image = CType(resources.GetObject("btcnip.Image"), System.Drawing.Image)
        Me.btcnip.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btcnip.Location = New System.Drawing.Point(189, 12)
        Me.btcnip.Name = "btcnip"
        Me.btcnip.Size = New System.Drawing.Size(32, 20)
        Me.btcnip.TabIndex = 1
        Me.btcnip.ToolTip = "Cari Data"
        '
        'tnama
        '
        Me.tnama.Enabled = False
        Me.tnama.Location = New System.Drawing.Point(83, 38)
        Me.tnama.Name = "tnama"
        Me.tnama.Size = New System.Drawing.Size(347, 20)
        Me.tnama.TabIndex = 3
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(43, 41)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(34, 13)
        Me.LabelControl1.TabIndex = 4
        Me.LabelControl1.Text = "Nama :"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(32, 67)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(45, 13)
        Me.LabelControl2.TabIndex = 5
        Me.LabelControl2.Text = "Tanggal :"
        '
        'ttgl1
        '
        Me.ttgl1.EditValue = Nothing
        Me.ttgl1.Location = New System.Drawing.Point(83, 64)
        Me.ttgl1.Name = "ttgl1"
        Me.ttgl1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl1.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl1.Properties.Mask.EditMask = ""
        Me.ttgl1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl1.Size = New System.Drawing.Size(100, 20)
        Me.ttgl1.TabIndex = 2
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(51, 93)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(26, 13)
        Me.LabelControl3.TabIndex = 154
        Me.LabelControl3.Text = "Jam :"
        '
        'tjam1
        '
        Me.tjam1.EditValue = New Date(2013, 8, 17, 0, 0, 0, 0)
        Me.tjam1.Location = New System.Drawing.Point(83, 90)
        Me.tjam1.Name = "tjam1"
        Me.tjam1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tjam1.Size = New System.Drawing.Size(100, 20)
        Me.tjam1.TabIndex = 4
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(189, 93)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(15, 13)
        Me.LabelControl4.TabIndex = 156
        Me.LabelControl4.Text = "s.d"
        '
        'tjam2
        '
        Me.tjam2.EditValue = New Date(2013, 8, 17, 0, 0, 0, 0)
        Me.tjam2.Location = New System.Drawing.Point(211, 90)
        Me.tjam2.Name = "tjam2"
        Me.tjam2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tjam2.Size = New System.Drawing.Size(100, 20)
        Me.tjam2.TabIndex = 5
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(190, 67)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(15, 13)
        Me.LabelControl5.TabIndex = 158
        Me.LabelControl5.Text = "s.d"
        '
        'ttgl2
        '
        Me.ttgl2.EditValue = Nothing
        Me.ttgl2.Location = New System.Drawing.Point(211, 64)
        Me.ttgl2.Name = "ttgl2"
        Me.ttgl2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl2.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl2.Properties.Mask.EditMask = ""
        Me.ttgl2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl2.Size = New System.Drawing.Size(100, 20)
        Me.ttgl2.TabIndex = 3
        '
        'tcbostat
        '
        Me.tcbostat.Location = New System.Drawing.Point(83, 116)
        Me.tcbostat.Name = "tcbostat"
        Me.tcbostat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tcbostat.Properties.Items.AddRange(New Object() {"SAKIT", "IZIN", "ALPHA", "CUTI", "LAIN-LAIN"})
        Me.tcbostat.Size = New System.Drawing.Size(228, 20)
        Me.tcbostat.TabIndex = 6
        '
        'LabelControl6
        '
        Me.LabelControl6.Location = New System.Drawing.Point(50, 119)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl6.TabIndex = 161
        Me.LabelControl6.Text = "Stat :"
        '
        'tket
        '
        Me.tket.Location = New System.Drawing.Point(83, 142)
        Me.tket.Name = "tket"
        Me.tket.Size = New System.Drawing.Size(347, 20)
        Me.tket.TabIndex = 7
        '
        'LabelControl7
        '
        Me.LabelControl7.Location = New System.Drawing.Point(14, 145)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(63, 13)
        Me.LabelControl7.TabIndex = 163
        Me.LabelControl7.Text = "Keterangan :"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(274, 199)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 23)
        Me.btsimpan.TabIndex = 9
        Me.btsimpan.Text = "&Simpan"
        '
        'btselesai
        '
        Me.btselesai.Location = New System.Drawing.Point(355, 199)
        Me.btselesai.Name = "btselesai"
        Me.btselesai.Size = New System.Drawing.Size(75, 23)
        Me.btselesai.TabIndex = 10
        Me.btselesai.Text = "Se&lesai"
        '
        'LabelControl8
        '
        Me.LabelControl8.Location = New System.Drawing.Point(14, 178)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(191, 13)
        Me.LabelControl8.TabIndex = 166
        Me.LabelControl8.Text = "Masuk kedalam perhitungan kehadiran ?"
        '
        'cstat
        '
        Me.cstat.EditValue = True
        Me.cstat.Location = New System.Drawing.Point(12, 197)
        Me.cstat.Name = "cstat"
        Me.cstat.Properties.Caption = "&Ya"
        Me.cstat.Size = New System.Drawing.Size(75, 19)
        Me.cstat.TabIndex = 8
        '
        'fstathadir2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(450, 228)
        Me.Controls.Add(Me.cstat)
        Me.Controls.Add(Me.LabelControl8)
        Me.Controls.Add(Me.btselesai)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.tket)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.ttgl2)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.tjam2)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.tjam1)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.ttgl1)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tnama)
        Me.Controls.Add(Me.btcnip)
        Me.Controls.Add(Me.tnip)
        Me.Controls.Add(Me.v)
        Me.Controls.Add(Me.tcbostat)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fstathadir2"
        Me.Text = "Ketidakhadiran Detail"
        CType(Me.tnip.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tjam1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tjam2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tcbostat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tket.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cstat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents v As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tnip As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btcnip As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tnama As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tjam1 As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tjam2 As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl2 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents tcbostat As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tket As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btselesai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cstat As DevExpress.XtraEditors.CheckEdit
End Class
