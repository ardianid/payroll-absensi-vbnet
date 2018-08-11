<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fnaik_gaji
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
        Me.ttgl = New DevExpress.XtraEditors.DateEdit()
        Me.grid1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cnip = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cnama = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cgaji_lama = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cgaji_baru = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btadd = New DevExpress.XtraEditors.SimpleButton()
        Me.btdel = New DevExpress.XtraEditors.SimpleButton()
        Me.btclear = New DevExpress.XtraEditors.SimpleButton()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        Me.GridSplitContainer1 = New DevExpress.XtraGrid.GridSplitContainer()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.ttgl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridSplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GridSplitContainer1.SuspendLayout()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(21, 15)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(45, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Tanggal :"
        '
        'ttgl
        '
        Me.ttgl.EditValue = Nothing
        Me.ttgl.Location = New System.Drawing.Point(72, 12)
        Me.ttgl.Name = "ttgl"
        Me.ttgl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl.Properties.Mask.EditMask = ""
        Me.ttgl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.ttgl.Size = New System.Drawing.Size(127, 20)
        Me.ttgl.TabIndex = 0
        '
        'grid1
        '
        Me.grid1.Cursor = System.Windows.Forms.Cursors.Default
        Me.grid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grid1.Location = New System.Drawing.Point(0, 0)
        Me.grid1.MainView = Me.GridView1
        Me.grid1.Name = "grid1"
        Me.grid1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.grid1.Size = New System.Drawing.Size(649, 268)
        Me.grid1.TabIndex = 6
        Me.grid1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cnip, Me.cnama, Me.cgaji_lama, Me.cgaji_baru, Me.GridColumn1})
        Me.GridView1.GridControl = Me.grid1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'cnip
        '
        Me.cnip.Caption = "Nip"
        Me.cnip.FieldName = "nip"
        Me.cnip.Name = "cnip"
        Me.cnip.OptionsColumn.AllowEdit = False
        Me.cnip.Visible = True
        Me.cnip.VisibleIndex = 0
        Me.cnip.Width = 68
        '
        'cnama
        '
        Me.cnama.Caption = "Nama"
        Me.cnama.FieldName = "nama"
        Me.cnama.Name = "cnama"
        Me.cnama.OptionsColumn.AllowEdit = False
        Me.cnama.Visible = True
        Me.cnama.VisibleIndex = 1
        Me.cnama.Width = 248
        '
        'cgaji_lama
        '
        Me.cgaji_lama.AppearanceCell.Options.UseTextOptions = True
        Me.cgaji_lama.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cgaji_lama.AppearanceHeader.Options.UseTextOptions = True
        Me.cgaji_lama.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cgaji_lama.Caption = "Gaji Lama"
        Me.cgaji_lama.DisplayFormat.FormatString = "n0"
        Me.cgaji_lama.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cgaji_lama.FieldName = "gapok"
        Me.cgaji_lama.Name = "cgaji_lama"
        Me.cgaji_lama.OptionsColumn.AllowEdit = False
        Me.cgaji_lama.Visible = True
        Me.cgaji_lama.VisibleIndex = 2
        Me.cgaji_lama.Width = 110
        '
        'cgaji_baru
        '
        Me.cgaji_baru.AppearanceCell.Options.UseTextOptions = True
        Me.cgaji_baru.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cgaji_baru.AppearanceHeader.Options.UseTextOptions = True
        Me.cgaji_baru.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cgaji_baru.Caption = "Gaji Baru"
        Me.cgaji_baru.DisplayFormat.FormatString = "n0"
        Me.cgaji_baru.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cgaji_baru.FieldName = "gapok2"
        Me.cgaji_baru.Name = "cgaji_baru"
        Me.cgaji_baru.OptionsColumn.AllowEdit = False
        Me.cgaji_baru.Visible = True
        Me.cgaji_baru.VisibleIndex = 3
        Me.cgaji_baru.Width = 113
        '
        'btadd
        '
        Me.btadd.Location = New System.Drawing.Point(12, 309)
        Me.btadd.Name = "btadd"
        Me.btadd.Size = New System.Drawing.Size(43, 23)
        Me.btadd.TabIndex = 1
        Me.btadd.Text = "&Add"
        '
        'btdel
        '
        Me.btdel.Location = New System.Drawing.Point(61, 309)
        Me.btdel.Name = "btdel"
        Me.btdel.Size = New System.Drawing.Size(43, 23)
        Me.btdel.TabIndex = 2
        Me.btdel.Text = "&Del"
        '
        'btclear
        '
        Me.btclear.Location = New System.Drawing.Point(110, 309)
        Me.btclear.Name = "btclear"
        Me.btclear.Size = New System.Drawing.Size(43, 23)
        Me.btclear.TabIndex = 3
        Me.btclear.Text = "&Clear"
        '
        'btclose
        '
        Me.btclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btclose.Location = New System.Drawing.Point(586, 317)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(75, 33)
        Me.btclose.TabIndex = 8
        Me.btclose.Text = "&Close"
        '
        'btsimpan
        '
        Me.btsimpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btsimpan.Location = New System.Drawing.Point(505, 317)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 33)
        Me.btsimpan.TabIndex = 7
        Me.btsimpan.Text = "&Simpan"
        '
        'GridSplitContainer1
        '
        Me.GridSplitContainer1.Grid = Me.grid1
        Me.GridSplitContainer1.Location = New System.Drawing.Point(12, 38)
        Me.GridSplitContainer1.Name = "GridSplitContainer1"
        Me.GridSplitContainer1.Panel1.Controls.Add(Me.grid1)
        Me.GridSplitContainer1.Size = New System.Drawing.Size(649, 268)
        Me.GridSplitContainer1.TabIndex = 9
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Hit Ulang Lembur"
        Me.GridColumn1.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.GridColumn1.FieldName = "hitlembur"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 4
        Me.GridColumn1.Width = 92
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = 1
        Me.RepositoryItemCheckEdit1.ValueUnchecked = 0
        '
        'fnaik_gaji
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(673, 359)
        Me.Controls.Add(Me.btclose)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.btclear)
        Me.Controls.Add(Me.btdel)
        Me.Controls.Add(Me.btadd)
        Me.Controls.Add(Me.GridSplitContainer1)
        Me.Controls.Add(Me.ttgl)
        Me.Controls.Add(Me.LabelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fnaik_gaji"
        Me.Text = "Naik Gaji"
        CType(Me.ttgl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridSplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GridSplitContainer1.ResumeLayout(False)
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents grid1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cnip As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cnama As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cgaji_lama As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cgaji_baru As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btdel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridSplitContainer1 As DevExpress.XtraGrid.GridSplitContainer
End Class
