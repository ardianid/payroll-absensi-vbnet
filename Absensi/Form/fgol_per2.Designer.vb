<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fgol_per2
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
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl2 = New DevExpress.XtraEditors.DateEdit()
        Me.ttgl1 = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.tkode = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.tnama = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl = New DevExpress.XtraEditors.DateEdit()
        Me.cbgol = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.grid1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.btdel = New DevExpress.XtraEditors.SimpleButton()
        Me.btadd = New DevExpress.XtraEditors.SimpleButton()
        Me.btselesai = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ttgl2.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl1.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbgol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl13
        '
        Me.LabelControl13.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.LabelControl13.Location = New System.Drawing.Point(215, 101)
        Me.LabelControl13.Name = "LabelControl13"
        Me.LabelControl13.Size = New System.Drawing.Size(15, 13)
        Me.LabelControl13.TabIndex = 56
        Me.LabelControl13.Text = "s.d"
        '
        'ttgl2
        '
        Me.ttgl2.EditValue = Nothing
        Me.ttgl2.Location = New System.Drawing.Point(244, 98)
        Me.ttgl2.Name = "ttgl2"
        Me.ttgl2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl2.Properties.Mask.EditMask = ""
        Me.ttgl2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl2.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl2.Size = New System.Drawing.Size(97, 20)
        Me.ttgl2.TabIndex = 51
        '
        'ttgl1
        '
        Me.ttgl1.EditValue = Nothing
        Me.ttgl1.Location = New System.Drawing.Point(107, 98)
        Me.ttgl1.Name = "ttgl1"
        Me.ttgl1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl1.Properties.Mask.EditMask = ""
        Me.ttgl1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl1.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl1.Size = New System.Drawing.Size(97, 20)
        Me.ttgl1.TabIndex = 50
        '
        'LabelControl12
        '
        Me.LabelControl12.Location = New System.Drawing.Point(58, 101)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Size = New System.Drawing.Size(43, 13)
        Me.LabelControl12.TabIndex = 55
        Me.LabelControl12.Text = "Periode :"
        '
        'LabelControl11
        '
        Me.LabelControl11.Location = New System.Drawing.Point(51, 77)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Size = New System.Drawing.Size(50, 13)
        Me.LabelControl11.TabIndex = 54
        Me.LabelControl11.Text = "Tgl Input :"
        '
        'tkode
        '
        Me.tkode.Location = New System.Drawing.Point(107, 22)
        Me.tkode.Name = "tkode"
        Me.tkode.Size = New System.Drawing.Size(285, 20)
        Me.tkode.TabIndex = 47
        '
        'LabelControl10
        '
        Me.LabelControl10.Location = New System.Drawing.Point(70, 25)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(31, 13)
        Me.LabelControl10.TabIndex = 53
        Me.LabelControl10.Text = "Kode :"
        '
        'tnama
        '
        Me.tnama.Location = New System.Drawing.Point(107, 48)
        Me.tnama.Name = "tnama"
        Me.tnama.Size = New System.Drawing.Size(285, 20)
        Me.tnama.TabIndex = 48
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(38, 51)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(63, 13)
        Me.LabelControl1.TabIndex = 52
        Me.LabelControl1.Text = "Keterangan :"
        '
        'ttgl
        '
        Me.ttgl.EditValue = Nothing
        Me.ttgl.Enabled = False
        Me.ttgl.Location = New System.Drawing.Point(107, 74)
        Me.ttgl.Name = "ttgl"
        Me.ttgl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl.Properties.Mask.EditMask = ""
        Me.ttgl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl.Size = New System.Drawing.Size(97, 20)
        Me.ttgl.TabIndex = 49
        '
        'cbgol
        '
        Me.cbgol.Location = New System.Drawing.Point(107, 124)
        Me.cbgol.Name = "cbgol"
        Me.cbgol.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbgol.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kode", 7, "Kode"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama", "Nama")})
        Me.cbgol.Properties.DisplayMember = "nama"
        Me.cbgol.Properties.NullText = ""
        Me.cbgol.Properties.ValueMember = "kode"
        Me.cbgol.Size = New System.Drawing.Size(285, 20)
        Me.cbgol.TabIndex = 57
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(49, 127)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(52, 13)
        Me.LabelControl2.TabIndex = 58
        Me.LabelControl2.Text = "Golongan :"
        '
        'grid1
        '
        Me.grid1.Location = New System.Drawing.Point(12, 150)
        Me.grid1.MainView = Me.GridView2
        Me.grid1.Name = "grid1"
        Me.grid1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.grid1.Size = New System.Drawing.Size(462, 220)
        Me.grid1.TabIndex = 129
        Me.grid1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6, Me.GridColumn7, Me.GridColumn1})
        Me.GridView2.GridControl = Me.grid1
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "NIP"
        Me.GridColumn6.FieldName = "nip"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        Me.GridColumn6.Width = 56
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Nama"
        Me.GridColumn7.FieldName = "nama"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 1
        Me.GridColumn7.Width = 205
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "OK?"
        Me.GridColumn1.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.GridColumn1.FieldName = "statok"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Width = 61
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.DisplayValueChecked = "1"
        Me.RepositoryItemCheckEdit1.DisplayValueUnchecked = "0"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = 1
        Me.RepositoryItemCheckEdit1.ValueUnchecked = 0
        '
        'btdel
        '
        Me.btdel.Location = New System.Drawing.Point(60, 376)
        Me.btdel.Name = "btdel"
        Me.btdel.Size = New System.Drawing.Size(42, 23)
        Me.btdel.TabIndex = 131
        Me.btdel.Text = "&Del"
        '
        'btadd
        '
        Me.btadd.Location = New System.Drawing.Point(12, 376)
        Me.btadd.Name = "btadd"
        Me.btadd.Size = New System.Drawing.Size(42, 23)
        Me.btadd.TabIndex = 130
        Me.btadd.Text = "&Add"
        '
        'btselesai
        '
        Me.btselesai.Location = New System.Drawing.Point(399, 376)
        Me.btselesai.Name = "btselesai"
        Me.btselesai.Size = New System.Drawing.Size(75, 25)
        Me.btselesai.TabIndex = 133
        Me.btselesai.Text = "Se&lesai"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(318, 376)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 25)
        Me.btsimpan.TabIndex = 132
        Me.btsimpan.Text = "&Simpan"
        '
        'fgol_per2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(487, 408)
        Me.ControlBox = False
        Me.Controls.Add(Me.btselesai)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.btdel)
        Me.Controls.Add(Me.btadd)
        Me.Controls.Add(Me.grid1)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.cbgol)
        Me.Controls.Add(Me.LabelControl13)
        Me.Controls.Add(Me.ttgl2)
        Me.Controls.Add(Me.ttgl1)
        Me.Controls.Add(Me.LabelControl12)
        Me.Controls.Add(Me.LabelControl11)
        Me.Controls.Add(Me.tkode)
        Me.Controls.Add(Me.LabelControl10)
        Me.Controls.Add(Me.tnama)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.ttgl)
        Me.Name = "fgol_per2"
        Me.Text = "Golongan Per-Periode"
        CType(Me.ttgl2.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl1.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbgol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl2 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents ttgl1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tkode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tnama As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents cbgol As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grid1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents btdel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btselesai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
End Class
