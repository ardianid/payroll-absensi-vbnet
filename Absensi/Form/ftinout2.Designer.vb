<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ftinout2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ftinout2))
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tnama = New DevExpress.XtraEditors.TextEdit()
        Me.btcnip = New DevExpress.XtraEditors.SimpleButton()
        Me.tnip = New DevExpress.XtraEditors.TextEdit()
        Me.v = New DevExpress.XtraEditors.LabelControl()
        Me.tjam1 = New DevExpress.XtraEditors.TimeEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl1 = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.btselesai = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.tket = New DevExpress.XtraEditors.TextEdit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnip.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tjam1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tket.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(49, 51)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(34, 13)
        Me.LabelControl1.TabIndex = 170
        Me.LabelControl1.Text = "Nama :"
        '
        'tnama
        '
        Me.tnama.Enabled = False
        Me.tnama.Location = New System.Drawing.Point(89, 48)
        Me.tnama.Name = "tnama"
        Me.tnama.Size = New System.Drawing.Size(347, 20)
        Me.tnama.TabIndex = 169
        '
        'btcnip
        '
        Me.btcnip.Image = CType(resources.GetObject("btcnip.Image"), System.Drawing.Image)
        Me.btcnip.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btcnip.Location = New System.Drawing.Point(195, 22)
        Me.btcnip.Name = "btcnip"
        Me.btcnip.Size = New System.Drawing.Size(32, 20)
        Me.btcnip.TabIndex = 1
        Me.btcnip.ToolTip = "Cari Data"
        '
        'tnip
        '
        Me.tnip.Location = New System.Drawing.Point(89, 22)
        Me.tnip.Name = "tnip"
        Me.tnip.Size = New System.Drawing.Size(100, 20)
        Me.tnip.TabIndex = 0
        '
        'v
        '
        Me.v.Location = New System.Drawing.Point(61, 25)
        Me.v.Name = "v"
        Me.v.Size = New System.Drawing.Size(22, 13)
        Me.v.TabIndex = 168
        Me.v.Text = "Nip :"
        '
        'tjam1
        '
        Me.tjam1.EditValue = New Date(2013, 8, 17, 0, 0, 0, 0)
        Me.tjam1.Location = New System.Drawing.Point(89, 100)
        Me.tjam1.Name = "tjam1"
        Me.tjam1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tjam1.Size = New System.Drawing.Size(117, 20)
        Me.tjam1.TabIndex = 3
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(57, 103)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(26, 13)
        Me.LabelControl3.TabIndex = 174
        Me.LabelControl3.Text = "Jam :"
        '
        'ttgl1
        '
        Me.ttgl1.EditValue = Nothing
        Me.ttgl1.Location = New System.Drawing.Point(89, 74)
        Me.ttgl1.Name = "ttgl1"
        Me.ttgl1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl1.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl1.Properties.Mask.EditMask = ""
        Me.ttgl1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl1.Size = New System.Drawing.Size(117, 20)
        Me.ttgl1.TabIndex = 2
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(38, 77)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(45, 13)
        Me.LabelControl2.TabIndex = 173
        Me.LabelControl2.Text = "Tanggal :"
        '
        'btselesai
        '
        Me.btselesai.Location = New System.Drawing.Point(361, 179)
        Me.btselesai.Name = "btselesai"
        Me.btselesai.Size = New System.Drawing.Size(75, 23)
        Me.btselesai.TabIndex = 6
        Me.btselesai.Text = "Se&lesai"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(280, 179)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 23)
        Me.btsimpan.TabIndex = 5
        Me.btsimpan.Text = "&Simpan"
        '
        'LabelControl7
        '
        Me.LabelControl7.Location = New System.Drawing.Point(20, 129)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(63, 13)
        Me.LabelControl7.TabIndex = 178
        Me.LabelControl7.Text = "Keterangan :"
        '
        'tket
        '
        Me.tket.Location = New System.Drawing.Point(89, 126)
        Me.tket.Name = "tket"
        Me.tket.Size = New System.Drawing.Size(347, 20)
        Me.tket.TabIndex = 4
        '
        'ftinout2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(466, 211)
        Me.Controls.Add(Me.btselesai)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.tket)
        Me.Controls.Add(Me.tjam1)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.ttgl1)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tnama)
        Me.Controls.Add(Me.btcnip)
        Me.Controls.Add(Me.tnip)
        Me.Controls.Add(Me.v)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ftinout2"
        Me.Text = "Kekurangan Jam Absen"
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnip.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tjam1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tket.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tnama As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btcnip As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tnip As DevExpress.XtraEditors.TextEdit
    Friend WithEvents v As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tjam1 As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btselesai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tket As DevExpress.XtraEditors.TextEdit
End Class
