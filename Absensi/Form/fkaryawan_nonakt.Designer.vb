<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fkaryawan_nonakt
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tnip = New System.Windows.Forms.TextBox()
        Me.tnama = New System.Windows.Forms.TextBox()
        Me.tdepart = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ttgl = New DevExpress.XtraEditors.DateEdit()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.talasan = New System.Windows.Forms.TextBox()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ttgl.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(55, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "NIP :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(45, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Nama :"
        '
        'tnip
        '
        Me.tnip.Enabled = False
        Me.tnip.Location = New System.Drawing.Point(92, 12)
        Me.tnip.Name = "tnip"
        Me.tnip.Size = New System.Drawing.Size(238, 21)
        Me.tnip.TabIndex = 2
        '
        'tnama
        '
        Me.tnama.Enabled = False
        Me.tnama.Location = New System.Drawing.Point(92, 39)
        Me.tnama.Name = "tnama"
        Me.tnama.Size = New System.Drawing.Size(238, 21)
        Me.tnama.TabIndex = 3
        '
        'tdepart
        '
        Me.tdepart.Enabled = False
        Me.tdepart.Location = New System.Drawing.Point(92, 66)
        Me.tdepart.Name = "tdepart"
        Me.tdepart.Size = New System.Drawing.Size(238, 21)
        Me.tdepart.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(39, 69)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Depart :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(25, 95)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Tgl Keluar :"
        '
        'ttgl
        '
        Me.ttgl.EditValue = Nothing
        Me.ttgl.Location = New System.Drawing.Point(92, 92)
        Me.ttgl.Name = "ttgl"
        Me.ttgl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl.Properties.Mask.EditMask = ""
        Me.ttgl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl.Size = New System.Drawing.Size(107, 20)
        Me.ttgl.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 121)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Alasan Keluar :"
        '
        'talasan
        '
        Me.talasan.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.talasan.Location = New System.Drawing.Point(92, 118)
        Me.talasan.Name = "talasan"
        Me.talasan.Size = New System.Drawing.Size(238, 21)
        Me.talasan.TabIndex = 1
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(255, 165)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 29)
        Me.btclose.TabIndex = 3
        Me.btclose.Text = "&Keluar"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(174, 165)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 29)
        Me.btsimpan.TabIndex = 2
        Me.btsimpan.Text = "&Simpan"
        '
        'fkaryawan_nonakt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(348, 203)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.talasan)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ttgl)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tdepart)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tnama)
        Me.Controls.Add(Me.tnip)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fkaryawan_nonakt"
        Me.Text = "Non Aktifkan Karyawan"
        CType(Me.ttgl.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tnip As System.Windows.Forms.TextBox
    Friend WithEvents tnama As System.Windows.Forms.TextBox
    Friend WithEvents tdepart As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ttgl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents talasan As System.Windows.Forms.TextBox
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
End Class
