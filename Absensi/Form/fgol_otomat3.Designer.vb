<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fgol_otomat3
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
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tmin = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tmax = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tupah = New DevExpress.XtraEditors.TextEdit()
        Me.bttambah = New DevExpress.XtraEditors.SimpleButton()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.CheckEdit1 = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.tjenis = New DevExpress.XtraEditors.TextEdit()
        CType(Me.tmin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tmax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tupah.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CheckEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tjenis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(45, 47)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(62, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Pencapaian :"
        '
        'tmin
        '
        Me.tmin.Location = New System.Drawing.Point(113, 40)
        Me.tmin.Name = "tmin"
        Me.tmin.Properties.Appearance.Options.UseTextOptions = True
        Me.tmin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.tmin.Properties.DisplayFormat.FormatString = "n0"
        Me.tmin.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tmin.Properties.EditFormat.FormatString = "n0"
        Me.tmin.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tmin.Properties.Mask.EditMask = "n0"
        Me.tmin.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tmin.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tmin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tmin.Size = New System.Drawing.Size(101, 20)
        Me.tmin.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(220, 44)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(15, 13)
        Me.LabelControl2.TabIndex = 6
        Me.LabelControl2.Text = "s.d"
        '
        'tmax
        '
        Me.tmax.Location = New System.Drawing.Point(252, 40)
        Me.tmax.Name = "tmax"
        Me.tmax.Properties.Appearance.Options.UseTextOptions = True
        Me.tmax.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.tmax.Properties.DisplayFormat.FormatString = "n0"
        Me.tmax.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tmax.Properties.EditFormat.FormatString = "n0"
        Me.tmax.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tmax.Properties.Mask.EditMask = "n0"
        Me.tmax.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tmax.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tmax.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tmax.Size = New System.Drawing.Size(101, 20)
        Me.tmax.TabIndex = 2
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(75, 66)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(32, 13)
        Me.LabelControl3.TabIndex = 8
        Me.LabelControl3.Text = "Upah :"
        '
        'tupah
        '
        Me.tupah.Location = New System.Drawing.Point(113, 63)
        Me.tupah.Name = "tupah"
        Me.tupah.Properties.Appearance.Options.UseTextOptions = True
        Me.tupah.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.tupah.Properties.DisplayFormat.FormatString = "n0"
        Me.tupah.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tupah.Properties.EditFormat.FormatString = "n0"
        Me.tupah.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tupah.Properties.Mask.EditMask = "n1"
        Me.tupah.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.tupah.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.tupah.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tupah.Size = New System.Drawing.Size(240, 20)
        Me.tupah.TabIndex = 3
        '
        'bttambah
        '
        Me.bttambah.Location = New System.Drawing.Point(229, 113)
        Me.bttambah.Name = "bttambah"
        Me.bttambah.Size = New System.Drawing.Size(59, 23)
        Me.bttambah.TabIndex = 5
        Me.bttambah.Text = "&Tambah"
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(294, 113)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(59, 23)
        Me.btclose.TabIndex = 6
        Me.btclose.Text = "&Close"
        '
        'CheckEdit1
        '
        Me.CheckEdit1.Location = New System.Drawing.Point(111, 89)
        Me.CheckEdit1.Name = "CheckEdit1"
        Me.CheckEdit1.Properties.Caption = "&Perkalian"
        Me.CheckEdit1.Size = New System.Drawing.Size(75, 19)
        Me.CheckEdit1.TabIndex = 4
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(25, 21)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(82, 13)
        Me.LabelControl4.TabIndex = 13
        Me.LabelControl4.Text = "Jenis Pekerjaan :"
        '
        'tjenis
        '
        Me.tjenis.Enabled = False
        Me.tjenis.Location = New System.Drawing.Point(113, 18)
        Me.tjenis.Name = "tjenis"
        Me.tjenis.Size = New System.Drawing.Size(240, 20)
        Me.tjenis.TabIndex = 0
        '
        'fgol_otomat3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(378, 151)
        Me.ControlBox = False
        Me.Controls.Add(Me.tjenis)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.CheckEdit1)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.bttambah)
        Me.Controls.Add(Me.tupah)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.tmax)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.tmin)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "fgol_otomat3"
        Me.Text = "Golongan Kerja (Range)"
        CType(Me.tmin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tmax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tupah.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CheckEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tjenis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tmin As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tmax As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tupah As DevExpress.XtraEditors.TextEdit
    Friend WithEvents bttambah As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CheckEdit1 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tjenis As DevExpress.XtraEditors.TextEdit
End Class
