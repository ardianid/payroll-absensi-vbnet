<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fbon2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fbon2))
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tbukti = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tnama = New DevExpress.XtraEditors.TextEdit()
        Me.btcnip = New DevExpress.XtraEditors.SimpleButton()
        Me.tnip = New DevExpress.XtraEditors.TextEdit()
        Me.v = New DevExpress.XtraEditors.LabelControl()
        Me.lblgapok = New DevExpress.XtraEditors.LabelControl()
        Me.tjumlah = New DevExpress.XtraEditors.TextEdit()
        Me.tket = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.btselesai = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tbukti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnip.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tjumlah.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tket.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(47, 21)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "No Bukti :"
        '
        'tbukti
        '
        Me.tbukti.Enabled = False
        Me.tbukti.Location = New System.Drawing.Point(99, 18)
        Me.tbukti.Name = "tbukti"
        Me.tbukti.Size = New System.Drawing.Size(100, 20)
        Me.tbukti.TabIndex = 1
        '
        'LabelControl11
        '
        Me.LabelControl11.Location = New System.Drawing.Point(48, 47)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Size = New System.Drawing.Size(45, 13)
        Me.LabelControl11.TabIndex = 44
        Me.LabelControl11.Text = "Tanggal :"
        '
        'ttgl
        '
        Me.ttgl.EditValue = Nothing
        Me.ttgl.Location = New System.Drawing.Point(99, 44)
        Me.ttgl.Name = "ttgl"
        Me.ttgl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl.Properties.Mask.EditMask = ""
        Me.ttgl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl.Size = New System.Drawing.Size(100, 20)
        Me.ttgl.TabIndex = 0
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(59, 99)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(34, 13)
        Me.LabelControl2.TabIndex = 170
        Me.LabelControl2.Text = "Nama :"
        '
        'tnama
        '
        Me.tnama.Enabled = False
        Me.tnama.Location = New System.Drawing.Point(99, 96)
        Me.tnama.Name = "tnama"
        Me.tnama.Size = New System.Drawing.Size(261, 20)
        Me.tnama.TabIndex = 169
        '
        'btcnip
        '
        Me.btcnip.Image = CType(resources.GetObject("btcnip.Image"), System.Drawing.Image)
        Me.btcnip.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btcnip.Location = New System.Drawing.Point(205, 70)
        Me.btcnip.Name = "btcnip"
        Me.btcnip.Size = New System.Drawing.Size(32, 20)
        Me.btcnip.TabIndex = 167
        Me.btcnip.ToolTip = "Cari Data"
        '
        'tnip
        '
        Me.tnip.Location = New System.Drawing.Point(99, 70)
        Me.tnip.Name = "tnip"
        Me.tnip.Size = New System.Drawing.Size(100, 20)
        Me.tnip.TabIndex = 1
        '
        'v
        '
        Me.v.Location = New System.Drawing.Point(71, 73)
        Me.v.Name = "v"
        Me.v.Size = New System.Drawing.Size(22, 13)
        Me.v.TabIndex = 168
        Me.v.Text = "Nip :"
        '
        'lblgapok
        '
        Me.lblgapok.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblgapok.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblgapok.Location = New System.Drawing.Point(35, 123)
        Me.lblgapok.Name = "lblgapok"
        Me.lblgapok.Size = New System.Drawing.Size(58, 17)
        Me.lblgapok.TabIndex = 172
        Me.lblgapok.Text = "Jumlah :"
        '
        'tjumlah
        '
        Me.tjumlah.Location = New System.Drawing.Point(99, 122)
        Me.tjumlah.Name = "tjumlah"
        Me.tjumlah.Properties.Appearance.Options.UseTextOptions = True
        Me.tjumlah.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.tjumlah.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tjumlah.Properties.DisplayFormat.FormatString = "n0"
        Me.tjumlah.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tjumlah.Properties.EditFormat.FormatString = "n0"
        Me.tjumlah.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tjumlah.Properties.Mask.EditMask = "n0"
        Me.tjumlah.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tjumlah.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tjumlah.Size = New System.Drawing.Size(100, 20)
        Me.tjumlah.TabIndex = 2
        '
        'tket
        '
        Me.tket.Location = New System.Drawing.Point(99, 148)
        Me.tket.Name = "tket"
        Me.tket.Size = New System.Drawing.Size(261, 20)
        Me.tket.TabIndex = 3
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(30, 151)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(63, 13)
        Me.LabelControl3.TabIndex = 174
        Me.LabelControl3.Text = "Keterangan :"
        '
        'btselesai
        '
        Me.btselesai.Location = New System.Drawing.Point(285, 198)
        Me.btselesai.Name = "btselesai"
        Me.btselesai.Size = New System.Drawing.Size(75, 25)
        Me.btselesai.TabIndex = 5
        Me.btselesai.Text = "Se&lesai"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(204, 198)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 25)
        Me.btsimpan.TabIndex = 4
        Me.btsimpan.Text = "&Simpan"
        '
        'fbon2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(390, 233)
        Me.ControlBox = False
        Me.Controls.Add(Me.btselesai)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.tket)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.lblgapok)
        Me.Controls.Add(Me.tjumlah)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.tnama)
        Me.Controls.Add(Me.btcnip)
        Me.Controls.Add(Me.tnip)
        Me.Controls.Add(Me.v)
        Me.Controls.Add(Me.LabelControl11)
        Me.Controls.Add(Me.ttgl)
        Me.Controls.Add(Me.tbukti)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "fbon2"
        Me.Text = "Pinjaman Karyawan (Bon)"
        CType(Me.tbukti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnip.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tjumlah.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tket.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbukti As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tnama As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btcnip As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tnip As DevExpress.XtraEditors.TextEdit
    Friend WithEvents v As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblgapok As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tjumlah As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tket As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btselesai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
End Class
