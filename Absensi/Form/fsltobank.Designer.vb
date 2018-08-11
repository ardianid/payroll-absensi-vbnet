<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fsltobank
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
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.cbbank = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cbbulan = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.ttahun = New DevExpress.XtraEditors.TextEdit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.cbbank.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbbulan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttahun.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btclose)
        Me.PanelControl1.Controls.Add(Me.btprev)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.cbbank)
        Me.PanelControl1.Controls.Add(Me.cbbulan)
        Me.PanelControl1.Controls.Add(Me.LabelControl2)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Controls.Add(Me.ttahun)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(274, 174)
        Me.PanelControl1.TabIndex = 0
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(187, 139)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 23)
        Me.btclose.TabIndex = 4
        Me.btclose.Text = "&Close"
        '
        'btprev
        '
        Me.btprev.Location = New System.Drawing.Point(106, 139)
        Me.btprev.Name = "btprev"
        Me.btprev.Size = New System.Drawing.Size(75, 23)
        Me.btprev.TabIndex = 3
        Me.btprev.Text = "&Preview"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(45, 85)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(30, 13)
        Me.LabelControl3.TabIndex = 8
        Me.LabelControl3.Text = "Bank :"
        '
        'cbbank
        '
        Me.cbbank.Location = New System.Drawing.Point(81, 82)
        Me.cbbank.Name = "cbbank"
        Me.cbbank.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbbank.Properties.Items.AddRange(New Object() {"All", "PANIN", "MANDIRI", "BNI", "BCA", "BRI", "PERMATA", "RABO"})
        Me.cbbank.Size = New System.Drawing.Size(148, 20)
        Me.cbbank.TabIndex = 2
        '
        'cbbulan
        '
        Me.cbbulan.Location = New System.Drawing.Point(81, 56)
        Me.cbbulan.Name = "cbbulan"
        Me.cbbulan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbbulan.Properties.Items.AddRange(New Object() {"Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "Nopember", "Desember"})
        Me.cbbulan.Size = New System.Drawing.Size(148, 20)
        Me.cbbulan.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(42, 59)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(33, 13)
        Me.LabelControl2.TabIndex = 6
        Me.LabelControl2.Text = "Bulan :"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(38, 33)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(37, 13)
        Me.LabelControl1.TabIndex = 4
        Me.LabelControl1.Text = "Tahun :"
        '
        'ttahun
        '
        Me.ttahun.Location = New System.Drawing.Point(81, 30)
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
        'fsltobank
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(274, 174)
        Me.Controls.Add(Me.PanelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fsltobank"
        Me.Text = "Rekap Gaji By Bank"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.cbbank.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbbulan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttahun.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents cbbulan As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttahun As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbbank As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btprev As DevExpress.XtraEditors.SimpleButton
End Class
