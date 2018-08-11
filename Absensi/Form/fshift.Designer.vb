<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fshift
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
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition()
        Me.cperiode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.SplitContainerControl3 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.grid1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btdel = New DevExpress.XtraEditors.SimpleButton()
        Me.btadd = New DevExpress.XtraEditors.SimpleButton()
        Me.SplitContainerControl2 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.grid2 = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rjam_kerja = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btdel2 = New DevExpress.XtraEditors.SimpleButton()
        Me.btad2 = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.SplitContainerControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl3.SuspendLayout()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.SuspendLayout()
        CType(Me.grid2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rjam_kerja, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cperiode
        '
        Me.cperiode.Caption = "Nama"
        Me.cperiode.FieldName = "nama_periode"
        Me.cperiode.Name = "cperiode"
        Me.cperiode.OptionsColumn.AllowEdit = False
        Me.cperiode.OptionsColumn.AllowFocus = False
        Me.cperiode.Visible = True
        Me.cperiode.VisibleIndex = 0
        Me.cperiode.Width = 115
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.SplitContainerControl3)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.SplitContainerControl2)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(876, 401)
        Me.SplitContainerControl1.SplitterPosition = 335
        Me.SplitContainerControl1.TabIndex = 0
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'SplitContainerControl3
        '
        Me.SplitContainerControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl3.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2
        Me.SplitContainerControl3.Horizontal = False
        Me.SplitContainerControl3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl3.Name = "SplitContainerControl3"
        Me.SplitContainerControl3.Panel1.Controls.Add(Me.grid1)
        Me.SplitContainerControl3.Panel1.Text = "Panel1"
        Me.SplitContainerControl3.Panel2.Controls.Add(Me.btdel)
        Me.SplitContainerControl3.Panel2.Controls.Add(Me.btadd)
        Me.SplitContainerControl3.Panel2.Text = "Panel2"
        Me.SplitContainerControl3.Size = New System.Drawing.Size(335, 401)
        Me.SplitContainerControl3.SplitterPosition = 36
        Me.SplitContainerControl3.TabIndex = 1
        Me.SplitContainerControl3.Text = "SplitContainerControl3"
        '
        'grid1
        '
        Me.grid1.Cursor = System.Windows.Forms.Cursors.Default
        Me.grid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grid1.Location = New System.Drawing.Point(0, 0)
        Me.grid1.MainView = Me.GridView1
        Me.grid1.Name = "grid1"
        Me.grid1.Size = New System.Drawing.Size(335, 360)
        Me.grid1.TabIndex = 0
        Me.grid1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4})
        Me.GridView1.GridControl = Me.grid1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Nama"
        Me.GridColumn1.FieldName = "nama_shift"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 97
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tgl Mulai"
        Me.GridColumn2.FieldName = "tgl_mulai"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Width = 62
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.Caption = "Periode"
        Me.GridColumn3.FieldName = "periode"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 44
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Perputaran"
        Me.GridColumn4.FieldName = "perputaran"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        Me.GridColumn4.Width = 71
        '
        'btdel
        '
        Me.btdel.Location = New System.Drawing.Point(89, 6)
        Me.btdel.Name = "btdel"
        Me.btdel.Size = New System.Drawing.Size(75, 23)
        Me.btdel.TabIndex = 2
        Me.btdel.Text = "&Hapus"
        '
        'btadd
        '
        Me.btadd.Location = New System.Drawing.Point(8, 6)
        Me.btadd.Name = "btadd"
        Me.btadd.Size = New System.Drawing.Size(75, 23)
        Me.btadd.TabIndex = 1
        Me.btadd.Text = "&Tambah"
        '
        'SplitContainerControl2
        '
        Me.SplitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2
        Me.SplitContainerControl2.Horizontal = False
        Me.SplitContainerControl2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl2.Name = "SplitContainerControl2"
        Me.SplitContainerControl2.Panel1.Controls.Add(Me.grid2)
        Me.SplitContainerControl2.Panel1.Text = "Panel1"
        Me.SplitContainerControl2.Panel2.Controls.Add(Me.btdel2)
        Me.SplitContainerControl2.Panel2.Controls.Add(Me.btad2)
        Me.SplitContainerControl2.Panel2.Text = "Panel2"
        Me.SplitContainerControl2.Size = New System.Drawing.Size(536, 401)
        Me.SplitContainerControl2.SplitterPosition = 36
        Me.SplitContainerControl2.TabIndex = 2
        Me.SplitContainerControl2.Text = "SplitContainerControl2"
        '
        'grid2
        '
        Me.grid2.Cursor = System.Windows.Forms.Cursors.Default
        Me.grid2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grid2.Location = New System.Drawing.Point(0, 0)
        Me.grid2.MainView = Me.GridView2
        Me.grid2.Name = "grid2"
        Me.grid2.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rjam_kerja})
        Me.grid2.Size = New System.Drawing.Size(536, 360)
        Me.grid2.TabIndex = 1
        Me.grid2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cperiode, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8})
        StyleFormatCondition1.Appearance.BackColor = System.Drawing.Color.Lime
        StyleFormatCondition1.Appearance.Options.UseBackColor = True
        StyleFormatCondition1.ApplyToRow = True
        StyleFormatCondition1.Column = Me.cperiode
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition1.Value1 = "MINGGU"
        Me.GridView2.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.GridView2.GridControl = Me.grid2
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Jam Kerja"
        Me.GridColumn6.ColumnEdit = Me.rjam_kerja
        Me.GridColumn6.FieldName = "kd_jam"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 1
        Me.GridColumn6.Width = 121
        '
        'rjam_kerja
        '
        Me.rjam_kerja.AutoHeight = False
        Me.rjam_kerja.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rjam_kerja.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kode", "Jm Kerja")})
        Me.rjam_kerja.DisplayMember = "kode"
        Me.rjam_kerja.Name = "rjam_kerja"
        Me.rjam_kerja.NullText = ""
        Me.rjam_kerja.ValueMember = "kode"
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Jm Masuk"
        Me.GridColumn7.FieldName = "jammasuk"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 2
        Me.GridColumn7.Width = 161
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Jm Pulang"
        Me.GridColumn8.FieldName = "jampulang"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 3
        Me.GridColumn8.Width = 164
        '
        'btdel2
        '
        Me.btdel2.Location = New System.Drawing.Point(103, 6)
        Me.btdel2.Name = "btdel2"
        Me.btdel2.Size = New System.Drawing.Size(75, 23)
        Me.btdel2.TabIndex = 4
        Me.btdel2.Text = "&Hapus All"
        '
        'btad2
        '
        Me.btad2.Location = New System.Drawing.Point(3, 6)
        Me.btad2.Name = "btad2"
        Me.btad2.Size = New System.Drawing.Size(94, 23)
        Me.btad2.TabIndex = 3
        Me.btad2.Text = "&Tambah"
        '
        'fshift
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(876, 401)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Name = "fshift"
        Me.Text = "Shift Karyawan "
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.SplitContainerControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl3.ResumeLayout(False)
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.ResumeLayout(False)
        CType(Me.grid2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rjam_kerja, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents SplitContainerControl3 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents SplitContainerControl2 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents grid1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grid2 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cperiode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btdel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btad2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents rjam_kerja As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents btdel2 As DevExpress.XtraEditors.SimpleButton
End Class
