<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fkalk_jamsostek
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
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ttahun = New DevExpress.XtraEditors.TextEdit()
        Me.cbbulan = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btcalc = New DevExpress.XtraEditors.SimpleButton()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ttahun.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbbulan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(19, 15)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(37, 13)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Tahun :"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(23, 41)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(33, 13)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Bulan :"
        '
        'ttahun
        '
        Me.ttahun.Location = New System.Drawing.Point(66, 12)
        Me.ttahun.Name = "ttahun"
        Me.ttahun.Properties.DisplayFormat.FormatString = "f0"
        Me.ttahun.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ttahun.Properties.EditFormat.FormatString = "f0"
        Me.ttahun.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ttahun.Properties.Mask.EditMask = "f0"
        Me.ttahun.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.ttahun.Size = New System.Drawing.Size(69, 20)
        Me.ttahun.TabIndex = 0
        '
        'cbbulan
        '
        Me.cbbulan.Location = New System.Drawing.Point(66, 38)
        Me.cbbulan.Name = "cbbulan"
        Me.cbbulan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbbulan.Properties.Items.AddRange(New Object() {"Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "Nopember", "Desember"})
        Me.cbbulan.Size = New System.Drawing.Size(173, 20)
        Me.cbbulan.TabIndex = 1
        '
        'btcalc
        '
        Me.btcalc.Location = New System.Drawing.Point(120, 83)
        Me.btcalc.Name = "btcalc"
        Me.btcalc.Size = New System.Drawing.Size(56, 23)
        Me.btcalc.TabIndex = 2
        Me.btcalc.Text = "&Calc"
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(182, 83)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(56, 23)
        Me.btclose.TabIndex = 3
        Me.btclose.Text = "&Keluar"
        '
        'fkalk_jamsostek
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(259, 115)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btcalc)
        Me.Controls.Add(Me.cbbulan)
        Me.Controls.Add(Me.ttahun)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fkalk_jamsostek"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Kalkulasi Iuran Jamsostek"
        CType(Me.ttahun.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbbulan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttahun As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cbbulan As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btcalc As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
End Class
