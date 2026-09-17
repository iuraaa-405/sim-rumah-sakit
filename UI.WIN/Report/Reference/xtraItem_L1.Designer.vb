<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class xtraItem_L1
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrLabel43 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel25 = New DevExpress.XtraReports.UI.XRLabel()
        Me.lMEMO = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.lTITLE = New DevExpress.XtraReports.UI.XRLabel()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.bindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.chkISACTIVE = New DevExpress.XtraReports.UI.XRCheckBox()
        Me.lISACTIVE = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        CType(Me.bindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel2, Me.lISACTIVE, Me.XrLabel43, Me.XrLabel25, Me.lMEMO, Me.chkISACTIVE})
        Me.Detail.HeightF = 41.20833!
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel43
        '
        Me.XrLabel43.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "MEMO")})
        Me.XrLabel43.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.XrLabel43.LocationFloat = New DevExpress.Utils.PointFloat(158.9583!, 0.0!)
        Me.XrLabel43.Name = "XrLabel43"
        Me.XrLabel43.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel43.SizeF = New System.Drawing.SizeF(458.0416!, 18.0!)
        Me.XrLabel43.StylePriority.UseFont = False
        Me.XrLabel43.Text = "XrLabel43"
        '
        'XrLabel25
        '
        Me.XrLabel25.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.XrLabel25.LocationFloat = New DevExpress.Utils.PointFloat(151.6667!, 0.0!)
        Me.XrLabel25.Name = "XrLabel25"
        Me.XrLabel25.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel25.SizeF = New System.Drawing.SizeF(7.291656!, 18.0!)
        Me.XrLabel25.StylePriority.UseFont = False
        Me.XrLabel25.Text = ":"
        '
        'lMEMO
        '
        Me.lMEMO.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lMEMO.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 0.0!)
        Me.lMEMO.Name = "lMEMO"
        Me.lMEMO.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lMEMO.SizeF = New System.Drawing.SizeF(141.6667!, 18.0!)
        Me.lMEMO.StylePriority.UseFont = False
        Me.lMEMO.Text = "Memo"
        '
        'TopMargin
        '
        Me.TopMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.lTITLE})
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'lTITLE
        '
        Me.lTITLE.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lTITLE.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 75.0!)
        Me.lTITLE.Name = "lTITLE"
        Me.lTITLE.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lTITLE.SizeF = New System.Drawing.SizeF(607.0001!, 18.0!)
        Me.lTITLE.StylePriority.UseBorders = False
        Me.lTITLE.StylePriority.UseFont = False
        Me.lTITLE.StylePriority.UseTextAlignment = False
        Me.lTITLE.Text = "Item_L1"
        Me.lTITLE.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'BottomMargin
        '
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'bindingSource
        '
        Me.bindingSource.DataSource = GetType(DataAccess.M_ITEM_L1)
        '
        'chkISACTIVE
        '
        Me.chkISACTIVE.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("CheckState", Nothing, "ISACTIVE")})
        Me.chkISACTIVE.LocationFloat = New DevExpress.Utils.PointFloat(158.9583!, 18.0!)
        Me.chkISACTIVE.Name = "chkISACTIVE"
        Me.chkISACTIVE.SizeF = New System.Drawing.SizeF(147.9167!, 18.00001!)
        Me.chkISACTIVE.Text = "ISACTIVE"
        '
        'lISACTIVE
        '
        Me.lISACTIVE.Font = New System.Drawing.Font("Times New Roman", 9.75!)
        Me.lISACTIVE.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 18.0!)
        Me.lISACTIVE.Name = "lISACTIVE"
        Me.lISACTIVE.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lISACTIVE.SizeF = New System.Drawing.SizeF(141.6667!, 18.0!)
        Me.lISACTIVE.StylePriority.UseFont = False
        Me.lISACTIVE.Text = "Active"
        '
        'XrLabel2
        '
        Me.XrLabel2.Font = New System.Drawing.Font("Times New Roman", 9.75!)
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(151.6667!, 18.0!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(7.291656!, 18.0!)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.Text = ":"
        '
        'xtraItem_L1
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin})
        Me.DataSource = Me.bindingSource
        Me.PageHeight = 1169
        Me.PageWidth = 827
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "12.1"
        CType(Me.bindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents bindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents lTITLE As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lMEMO As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel25 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel43 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents chkISACTIVE As DevExpress.XtraReports.UI.XRCheckBox
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lISACTIVE As DevExpress.XtraReports.UI.XRLabel
End Class
