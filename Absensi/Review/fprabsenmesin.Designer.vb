<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fprabsenmesin
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
        Me.components = New System.ComponentModel.Container()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.DropDownButton1 = New DevExpress.XtraEditors.DropDownButton()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.btexcel2007 = New DevExpress.XtraBars.BarButtonItem()
        Me.btexcel = New DevExpress.XtraBars.BarButtonItem()
        Me.bthtml = New DevExpress.XtraBars.BarButtonItem()
        Me.btrtf = New DevExpress.XtraBars.BarButtonItem()
        Me.btpdf = New DevExpress.XtraBars.BarButtonItem()
        Me.bttext = New DevExpress.XtraBars.BarButtonItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.btload = New DevExpress.XtraEditors.SimpleButton()
        Me.cbpeg = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.cbgolongan = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl2 = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ttgl1 = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.grid1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cnip = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cnama = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cgol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ctgl = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cjammasuk = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rall = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbpeg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbgolongan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttgl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rall, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Horizontal = False
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.SplitContainerControl1.Panel1.CaptionLocation = DevExpress.Utils.Locations.Top
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.DropDownButton1)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.btload)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.cbpeg)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.LabelControl4)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.cbgolongan)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.LabelControl3)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.ttgl2)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.LabelControl2)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.ttgl1)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.LabelControl1)
        Me.SplitContainerControl1.Panel1.ShowCaption = True
        Me.SplitContainerControl1.Panel1.Text = "Kriteria && Command"
        Me.SplitContainerControl1.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.SplitContainerControl1.Panel2.CaptionLocation = DevExpress.Utils.Locations.Left
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.grid1)
        Me.SplitContainerControl1.Panel2.ShowCaption = True
        Me.SplitContainerControl1.Panel2.Text = "Detail Presensi"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(896, 485)
        Me.SplitContainerControl1.SplitterPosition = 63
        Me.SplitContainerControl1.TabIndex = 3
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'DropDownButton1
        '
        Me.DropDownButton1.DropDownControl = Me.PopupMenu1
        Me.DropDownButton1.Location = New System.Drawing.Point(802, 6)
        Me.DropDownButton1.Name = "DropDownButton1"
        Me.DropDownButton1.Size = New System.Drawing.Size(84, 27)
        Me.DropDownButton1.TabIndex = 5
        Me.DropDownButton1.Text = "&Export To"
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btexcel2007), New DevExpress.XtraBars.LinkPersistInfo(Me.btexcel), New DevExpress.XtraBars.LinkPersistInfo(Me.bthtml), New DevExpress.XtraBars.LinkPersistInfo(Me.btrtf), New DevExpress.XtraBars.LinkPersistInfo(Me.btpdf), New DevExpress.XtraBars.LinkPersistInfo(Me.bttext)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'btexcel2007
        '
        Me.btexcel2007.Caption = "Export To Excel 2007"
        Me.btexcel2007.Id = 22
        Me.btexcel2007.Name = "btexcel2007"
        '
        'btexcel
        '
        Me.btexcel.Caption = "Export To Excel"
        Me.btexcel.Id = 23
        Me.btexcel.Name = "btexcel"
        '
        'bthtml
        '
        Me.bthtml.Caption = "Export To HTML"
        Me.bthtml.Id = 24
        Me.bthtml.Name = "bthtml"
        '
        'btrtf
        '
        Me.btrtf.Caption = "Export To RTF"
        Me.btrtf.Id = 25
        Me.btrtf.Name = "btrtf"
        '
        'btpdf
        '
        Me.btpdf.Caption = "Export To PDF"
        Me.btpdf.Id = 26
        Me.btpdf.Name = "btpdf"
        '
        'bttext
        '
        Me.bttext.Caption = "Export To Text"
        Me.bttext.Id = 27
        Me.bttext.Name = "bttext"
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btexcel2007, Me.btexcel, Me.bthtml, Me.btrtf, Me.btpdf, Me.bttext})
        Me.BarManager1.MaxItemId = 28
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(896, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 485)
        Me.barDockControlBottom.Size = New System.Drawing.Size(896, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 485)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(896, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 485)
        '
        'btload
        '
        Me.btload.Location = New System.Drawing.Point(727, 6)
        Me.btload.Name = "btload"
        Me.btload.Size = New System.Drawing.Size(69, 27)
        Me.btload.TabIndex = 4
        Me.btload.Text = "&Load"
        '
        'cbpeg
        '
        Me.cbpeg.Location = New System.Drawing.Point(575, 10)
        Me.cbpeg.Name = "cbpeg"
        Me.cbpeg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbpeg.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nip", 13, "NIP"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama", "Nama")})
        Me.cbpeg.Properties.DisplayMember = "nama"
        Me.cbpeg.Properties.NullText = ""
        Me.cbpeg.Properties.ValueMember = "nip"
        Me.cbpeg.Size = New System.Drawing.Size(132, 20)
        Me.cbpeg.TabIndex = 3
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(522, 13)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(47, 13)
        Me.LabelControl4.TabIndex = 7
        Me.LabelControl4.Text = "Pegawai :"
        '
        'cbgolongan
        '
        Me.cbgolongan.Location = New System.Drawing.Point(366, 10)
        Me.cbgolongan.Name = "cbgolongan"
        Me.cbgolongan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbgolongan.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kode", "kode", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama", "Nama")})
        Me.cbgolongan.Properties.DisplayMember = "nama"
        Me.cbgolongan.Properties.NullText = ""
        Me.cbgolongan.Properties.ValueMember = "kode"
        Me.cbgolongan.Size = New System.Drawing.Size(132, 20)
        Me.cbgolongan.TabIndex = 2
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(308, 13)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(52, 13)
        Me.LabelControl3.TabIndex = 5
        Me.LabelControl3.Text = "Golongan :"
        '
        'ttgl2
        '
        Me.ttgl2.EditValue = Nothing
        Me.ttgl2.Location = New System.Drawing.Point(186, 10)
        Me.ttgl2.Name = "ttgl2"
        Me.ttgl2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl2.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl2.Size = New System.Drawing.Size(100, 20)
        Me.ttgl2.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(165, 13)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(15, 13)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "s.d"
        '
        'ttgl1
        '
        Me.ttgl1.EditValue = Nothing
        Me.ttgl1.Location = New System.Drawing.Point(59, 10)
        Me.ttgl1.Name = "ttgl1"
        Me.ttgl1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ttgl1.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ttgl1.Size = New System.Drawing.Size(100, 20)
        Me.ttgl1.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(10, 13)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(43, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Periode :"
        '
        'grid1
        '
        Me.grid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grid1.Location = New System.Drawing.Point(0, 0)
        Me.grid1.MainView = Me.GridView1
        Me.grid1.Name = "grid1"
        Me.grid1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rall})
        Me.grid1.Size = New System.Drawing.Size(873, 413)
        Me.grid1.TabIndex = 0
        Me.grid1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cnip, Me.cnama, Me.cgol, Me.ctgl, Me.cjammasuk, Me.GridColumn1})
        Me.GridView1.GridControl = Me.grid1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.AutoExpandAllGroups = True
        Me.GridView1.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.cnama, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.ctgl, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.cjammasuk, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'cnip
        '
        Me.cnip.Caption = "NIP"
        Me.cnip.FieldName = "nip"
        Me.cnip.Name = "cnip"
        Me.cnip.OptionsColumn.AllowEdit = False
        Me.cnip.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cnip.Width = 76
        '
        'cnama
        '
        Me.cnama.Caption = "Nama"
        Me.cnama.FieldName = "nama"
        Me.cnama.Name = "cnama"
        Me.cnama.OptionsColumn.AllowEdit = False
        Me.cnama.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cnama.Visible = True
        Me.cnama.VisibleIndex = 0
        Me.cnama.Width = 231
        '
        'cgol
        '
        Me.cgol.Caption = "Golongan"
        Me.cgol.FieldName = "golongan"
        Me.cgol.Name = "cgol"
        Me.cgol.OptionsColumn.AllowEdit = False
        Me.cgol.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cgol.Visible = True
        Me.cgol.VisibleIndex = 1
        Me.cgol.Width = 244
        '
        'ctgl
        '
        Me.ctgl.Caption = "Tanggal"
        Me.ctgl.DisplayFormat.FormatString = "d"
        Me.ctgl.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ctgl.FieldName = "tanggal"
        Me.ctgl.Name = "ctgl"
        Me.ctgl.OptionsColumn.AllowEdit = False
        Me.ctgl.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.ctgl.Visible = True
        Me.ctgl.VisibleIndex = 2
        Me.ctgl.Width = 112
        '
        'cjammasuk
        '
        Me.cjammasuk.Caption = "Jam Absen"
        Me.cjammasuk.DisplayFormat.FormatString = "t"
        Me.cjammasuk.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.cjammasuk.FieldName = "jam"
        Me.cjammasuk.Name = "cjammasuk"
        Me.cjammasuk.OptionsColumn.AllowEdit = False
        Me.cjammasuk.Visible = True
        Me.cjammasuk.VisibleIndex = 3
        Me.cjammasuk.Width = 123
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Kalk"
        Me.GridColumn1.ColumnEdit = Me.rall
        Me.GridColumn1.FieldName = "skalk"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 4
        '
        'rall
        '
        Me.rall.AutoHeight = False
        Me.rall.DisplayValueChecked = "1"
        Me.rall.DisplayValueUnchecked = "0"
        Me.rall.Name = "rall"
        Me.rall.ValueChecked = 1
        Me.rall.ValueUnchecked = 0
        '
        'fprabsenmesin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(896, 485)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "fprabsenmesin"
        Me.Text = "Laporan Absensi Mesin"
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbpeg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbgolongan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttgl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rall, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents DropDownButton1 As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents btload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cbpeg As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbgolongan As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl2 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ttgl1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grid1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cnip As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cnama As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cgol As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ctgl As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cjammasuk As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rall As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btexcel2007 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btexcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bthtml As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btrtf As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btpdf As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bttext As DevExpress.XtraBars.BarButtonItem
End Class
