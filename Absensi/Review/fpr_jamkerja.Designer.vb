<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fpr_jamkerja
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
        Me.tjenislap = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btload = New DevExpress.XtraEditors.SimpleButton()
        Me.ttgl2 = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl1 = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.tjenislap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tjenislap
        '
        Me.tjenislap.Location = New System.Drawing.Point(85, 47)
        Me.tjenislap.Name = "tjenislap"
        Me.tjenislap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tjenislap.Properties.Items.AddRange(New Object() {"LAP TOTAL", "LAP DETAIL"})
        '   Me.tjenislap.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tjenislap.Size = New System.Drawing.Size(227, 20)
        Me.tjenislap.TabIndex = 21
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(28, 50)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(51, 13)
        Me.LabelControl3.TabIndex = 26
        Me.LabelControl3.Text = "Jenis Lap :"
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(237, 99)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 26)
        Me.btclose.TabIndex = 23
        Me.btclose.Text = "&Keluar"
        '
        'btload
        '
        Me.btload.Location = New System.Drawing.Point(156, 99)
        Me.btload.Name = "btload"
        Me.btload.Size = New System.Drawing.Size(75, 26)
        Me.btload.TabIndex = 22
        Me.btload.Text = "&Load"
        '
        'ttgl2
        '
        Me.ttgl2.EditValue = Nothing
        Me.ttgl2.Location = New System.Drawing.Point(212, 21)
        Me.ttgl2.Name = "ttgl2"
        Me.ttgl2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl2.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl2.Size = New System.Drawing.Size(100, 20)
        Me.ttgl2.TabIndex = 20
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(191, 24)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(15, 13)
        Me.LabelControl2.TabIndex = 25
        Me.LabelControl2.Text = "s.d"
        '
        'ttgl1
        '
        Me.ttgl1.EditValue = Nothing
        Me.ttgl1.Location = New System.Drawing.Point(85, 21)
        Me.ttgl1.Name = "ttgl1"
        Me.ttgl1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl1.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl1.Size = New System.Drawing.Size(100, 20)
        Me.ttgl1.TabIndex = 19
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(36, 24)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(43, 13)
        Me.LabelControl1.TabIndex = 24
        Me.LabelControl1.Text = "Periode :"
        '
        'fpr_jamkerja
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(333, 137)
        Me.Controls.Add(Me.tjenislap)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btload)
        Me.Controls.Add(Me.ttgl2)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.ttgl1)
        Me.Controls.Add(Me.LabelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fpr_jamkerja"
        Me.Text = "Jam Kerja Karyawan"
        CType(Me.tjenislap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tjenislap As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ttgl2 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
