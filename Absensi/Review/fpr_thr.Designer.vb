<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fpr_thr
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fpr_thr))
        Me.cbdepart = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btload = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tnama = New DevExpress.XtraEditors.TextEdit()
        Me.btcnip = New DevExpress.XtraEditors.SimpleButton()
        Me.tnip = New DevExpress.XtraEditors.TextEdit()
        Me.v = New DevExpress.XtraEditors.LabelControl()
        CType(Me.cbdepart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnip.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cbdepart
        '
        Me.cbdepart.Location = New System.Drawing.Point(90, 12)
        Me.cbdepart.Name = "cbdepart"
        Me.cbdepart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbdepart.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama", "Nama")})
        Me.cbdepart.Properties.DisplayMember = "nama"
        Me.cbdepart.Properties.NullText = ""
        Me.cbdepart.Properties.PopupSizeable = False
        Me.cbdepart.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cbdepart.Properties.ValueMember = "nama"
        Me.cbdepart.Size = New System.Drawing.Size(219, 20)
        Me.cbdepart.TabIndex = 0
        '
        'LabelControl14
        '
        Me.LabelControl14.Location = New System.Drawing.Point(18, 15)
        Me.LabelControl14.Name = "LabelControl14"
        Me.LabelControl14.Size = New System.Drawing.Size(66, 13)
        Me.LabelControl14.TabIndex = 37
        Me.LabelControl14.Text = "Departemen :"
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(247, 117)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(62, 26)
        Me.btclose.TabIndex = 3
        Me.btclose.Text = "&Keluar"
        '
        'btload
        '
        Me.btload.Location = New System.Drawing.Point(179, 117)
        Me.btload.Name = "btload"
        Me.btload.Size = New System.Drawing.Size(62, 26)
        Me.btload.TabIndex = 2
        Me.btload.Text = "&Load"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(50, 67)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(34, 13)
        Me.LabelControl3.TabIndex = 199
        Me.LabelControl3.Text = "Nama :"
        '
        'tnama
        '
        Me.tnama.Enabled = False
        Me.tnama.Location = New System.Drawing.Point(90, 64)
        Me.tnama.Name = "tnama"
        Me.tnama.Size = New System.Drawing.Size(219, 20)
        Me.tnama.TabIndex = 198
        '
        'btcnip
        '
        Me.btcnip.Image = CType(resources.GetObject("btcnip.Image"), System.Drawing.Image)
        Me.btcnip.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.btcnip.Location = New System.Drawing.Point(196, 38)
        Me.btcnip.Name = "btcnip"
        Me.btcnip.Size = New System.Drawing.Size(32, 20)
        Me.btcnip.TabIndex = 196
        Me.btcnip.ToolTip = "Cari Data"
        '
        'tnip
        '
        Me.tnip.Location = New System.Drawing.Point(90, 38)
        Me.tnip.Name = "tnip"
        Me.tnip.Size = New System.Drawing.Size(100, 20)
        Me.tnip.TabIndex = 1
        '
        'v
        '
        Me.v.Location = New System.Drawing.Point(62, 41)
        Me.v.Name = "v"
        Me.v.Size = New System.Drawing.Size(22, 13)
        Me.v.TabIndex = 197
        Me.v.Text = "Nip :"
        '
        'fpr_thr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(344, 154)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btload)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.tnama)
        Me.Controls.Add(Me.btcnip)
        Me.Controls.Add(Me.tnip)
        Me.Controls.Add(Me.v)
        Me.Controls.Add(Me.cbdepart)
        Me.Controls.Add(Me.LabelControl14)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fpr_thr"
        Me.Text = "Laporan THR"
        CType(Me.cbdepart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnip.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbdepart As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tnama As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btcnip As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tnip As DevExpress.XtraEditors.TextEdit
    Friend WithEvents v As DevExpress.XtraEditors.LabelControl
End Class
