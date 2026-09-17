<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class xtraBuktiPenyetoran
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.lblREPORT = New DevExpress.XtraReports.UI.XRLabel()
        Me.bindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel()
        Me.YANGMENERIMA = New DevExpress.XtraReports.Parameters.Parameter()
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel()
        Me.PANGKATYANGMENERIMA = New DevExpress.XtraReports.Parameters.Parameter()
        Me.XrLabel21 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblTanggal = New DevExpress.XtraReports.UI.XRLabel()
        Me.TANGGALTANDATANGAN = New DevExpress.XtraReports.Parameters.Parameter()
        Me.XrLabel23 = New DevExpress.XtraReports.UI.XRLabel()
        Me.PANGKATYANGMENYETORKAN = New DevExpress.XtraReports.Parameters.Parameter()
        Me.XrLabel24 = New DevExpress.XtraReports.UI.XRLabel()
        Me.YANGMENYETORKAN = New DevExpress.XtraReports.Parameters.Parameter()
        Me.XrLabel25 = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblCATEGORY = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblTerbilang = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblTelahTerima = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.lPHONE = New DevExpress.XtraReports.UI.XRLabel()
        Me.lNPWP = New DevExpress.XtraReports.UI.XRLabel()
        Me.lADDRESS = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.sUntukKeperluan = New DevExpress.XtraReports.UI.CalculatedField()
        Me.TANGGAL = New DevExpress.XtraReports.Parameters.Parameter()
        CType(Me.bindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.HeightF = 0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 22.91667!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.HeightF = 21.875!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'lblREPORT
        '
        Me.lblREPORT.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblREPORT.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.lblREPORT.Name = "lblREPORT"
        Me.lblREPORT.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblREPORT.SizeF = New System.Drawing.SizeF(750.0!, 25.0!)
        Me.lblREPORT.StylePriority.UseFont = False
        Me.lblREPORT.StylePriority.UseTextAlignment = False
        Me.lblREPORT.Text = "BUKTI KAS MASUK"
        Me.lblREPORT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'bindingSource
        '
        Me.bindingSource.DataSource = GetType(DataAccess.F_SETOR_H)
        '
        'XrLabel9
        '
        Me.XrLabel9.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "KDSETOR")})
        Me.XrLabel9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(568.3403!, 46.16668!)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(181.6597!, 22.16666!)
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.StylePriority.UseTextAlignment = False
        Me.XrLabel9.Text = "XrLabel9"
        Me.XrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine1, Me.XrLabel19, Me.XrLabel20, Me.XrLabel21, Me.XrLabel22, Me.lblTanggal, Me.XrLabel23, Me.XrLabel24, Me.XrLabel25, Me.lblCATEGORY, Me.XrLabel16, Me.XrLabel17, Me.XrLabel18, Me.lblTerbilang, Me.XrLabel12, Me.XrLabel13, Me.XrLabel14, Me.lblTelahTerima, Me.XrLabel10, Me.XrLabel11, Me.XrLabel7, Me.XrLabel5, Me.XrLabel3, Me.XrLabel6, Me.XrLabel4, Me.XrLabel2, Me.lPHONE, Me.lNPWP, Me.lADDRESS, Me.XrLabel1, Me.lblREPORT, Me.XrLabel9})
        Me.ReportHeader.HeightF = 490.6667!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'XrLine1
        '
        Me.XrLine1.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLine1.BorderWidth = 0!
        Me.XrLine1.LineWidth = 2
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 142.125!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(750.0001!, 5.0!)
        Me.XrLine1.StylePriority.UseBorders = False
        Me.XrLine1.StylePriority.UseBorderWidth = False
        '
        'XrLabel19
        '
        Me.XrLabel19.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel19.CanShrink = True
        Me.XrLabel19.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.YANGMENERIMA, "Text", "")})
        Me.XrLabel19.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel19.LocationFloat = New DevExpress.Utils.PointFloat(471.2503!, 454.6667!)
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel19.SizeF = New System.Drawing.SizeF(278.7498!, 18.0!)
        Me.XrLabel19.StylePriority.UseBorders = False
        Me.XrLabel19.StylePriority.UseFont = False
        Me.XrLabel19.StylePriority.UseTextAlignment = False
        Me.XrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrLabel19.Visible = False
        '
        'YANGMENERIMA
        '
        Me.YANGMENERIMA.Description = "Menerima"
        Me.YANGMENERIMA.Name = "YANGMENERIMA"
        '
        'XrLabel20
        '
        Me.XrLabel20.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel20.CanShrink = True
        Me.XrLabel20.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.PANGKATYANGMENERIMA, "Text", "")})
        Me.XrLabel20.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel20.LocationFloat = New DevExpress.Utils.PointFloat(471.2503!, 472.6666!)
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel20.SizeF = New System.Drawing.SizeF(278.7498!, 18.0!)
        Me.XrLabel20.StylePriority.UseBorders = False
        Me.XrLabel20.StylePriority.UseFont = False
        Me.XrLabel20.StylePriority.UseTextAlignment = False
        Me.XrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrLabel20.Visible = False
        '
        'PANGKATYANGMENERIMA
        '
        Me.PANGKATYANGMENERIMA.Description = "Pangakt Menerima"
        Me.PANGKATYANGMENERIMA.Name = "PANGKATYANGMENERIMA"
        '
        'XrLabel21
        '
        Me.XrLabel21.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel21.CanShrink = True
        Me.XrLabel21.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel21.LocationFloat = New DevExpress.Utils.PointFloat(0!, 335.0!)
        Me.XrLabel21.Name = "XrLabel21"
        Me.XrLabel21.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel21.SizeF = New System.Drawing.SizeF(278.7498!, 18.0!)
        Me.XrLabel21.StylePriority.UseBorders = False
        Me.XrLabel21.StylePriority.UseFont = False
        Me.XrLabel21.StylePriority.UseTextAlignment = False
        Me.XrLabel21.Text = "Yang Menyetorkan"
        Me.XrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrLabel21.Visible = False
        '
        'XrLabel22
        '
        Me.XrLabel22.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel22.CanShrink = True
        Me.XrLabel22.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel22.LocationFloat = New DevExpress.Utils.PointFloat(471.2503!, 335.0!)
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel22.SizeF = New System.Drawing.SizeF(278.7498!, 18.0!)
        Me.XrLabel22.StylePriority.UseBorders = False
        Me.XrLabel22.StylePriority.UseFont = False
        Me.XrLabel22.StylePriority.UseTextAlignment = False
        Me.XrLabel22.Text = "Yang Menerima"
        Me.XrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrLabel22.Visible = False
        '
        'lblTanggal
        '
        Me.lblTanggal.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.lblTanggal.CanShrink = True
        Me.lblTanggal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.TANGGALTANDATANGAN, "Text", "")})
        Me.lblTanggal.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.lblTanggal.LocationFloat = New DevExpress.Utils.PointFloat(471.2503!, 317.0001!)
        Me.lblTanggal.Name = "lblTanggal"
        Me.lblTanggal.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblTanggal.SizeF = New System.Drawing.SizeF(278.7497!, 18.0!)
        Me.lblTanggal.StylePriority.UseBorders = False
        Me.lblTanggal.StylePriority.UseFont = False
        Me.lblTanggal.StylePriority.UseTextAlignment = False
        Me.lblTanggal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.lblTanggal.Visible = False
        '
        'TANGGALTANDATANGAN
        '
        Me.TANGGALTANDATANGAN.Description = "Tanggal TT"
        Me.TANGGALTANDATANGAN.Name = "TANGGALTANDATANGAN"
        '
        'XrLabel23
        '
        Me.XrLabel23.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel23.CanShrink = True
        Me.XrLabel23.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.PANGKATYANGMENYETORKAN, "Text", "")})
        Me.XrLabel23.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel23.LocationFloat = New DevExpress.Utils.PointFloat(0!, 472.6667!)
        Me.XrLabel23.Name = "XrLabel23"
        Me.XrLabel23.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel23.SizeF = New System.Drawing.SizeF(278.7498!, 18.0!)
        Me.XrLabel23.StylePriority.UseBorders = False
        Me.XrLabel23.StylePriority.UseFont = False
        Me.XrLabel23.StylePriority.UseTextAlignment = False
        Me.XrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrLabel23.Visible = False
        '
        'PANGKATYANGMENYETORKAN
        '
        Me.PANGKATYANGMENYETORKAN.Description = "Pangakat Menyetor"
        Me.PANGKATYANGMENYETORKAN.Name = "PANGKATYANGMENYETORKAN"
        '
        'XrLabel24
        '
        Me.XrLabel24.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel24.CanShrink = True
        Me.XrLabel24.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.YANGMENYETORKAN, "Text", "")})
        Me.XrLabel24.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel24.LocationFloat = New DevExpress.Utils.PointFloat(0!, 454.6667!)
        Me.XrLabel24.Name = "XrLabel24"
        Me.XrLabel24.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel24.SizeF = New System.Drawing.SizeF(278.7498!, 18.0!)
        Me.XrLabel24.StylePriority.UseBorders = False
        Me.XrLabel24.StylePriority.UseFont = False
        Me.XrLabel24.StylePriority.UseTextAlignment = False
        Me.XrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrLabel24.Visible = False
        '
        'YANGMENYETORKAN
        '
        Me.YANGMENYETORKAN.Description = "Menyetor"
        Me.YANGMENYETORKAN.Name = "YANGMENYETORKAN"
        '
        'XrLabel25
        '
        Me.XrLabel25.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel25.CanShrink = True
        Me.XrLabel25.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel25.LocationFloat = New DevExpress.Utils.PointFloat(471.2503!, 353.0!)
        Me.XrLabel25.Name = "XrLabel25"
        Me.XrLabel25.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel25.SizeF = New System.Drawing.SizeF(278.7498!, 18.0!)
        Me.XrLabel25.StylePriority.UseBorders = False
        Me.XrLabel25.StylePriority.UseFont = False
        Me.XrLabel25.StylePriority.UseTextAlignment = False
        Me.XrLabel25.Text = "Bendaharawan Penerimaan"
        Me.XrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrLabel25.Visible = False
        '
        'lblCATEGORY
        '
        Me.lblCATEGORY.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.lblCATEGORY.CanShrink = True
        Me.lblCATEGORY.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.lblCATEGORY.LocationFloat = New DevExpress.Utils.PointFloat(0!, 353.0!)
        Me.lblCATEGORY.Name = "lblCATEGORY"
        Me.lblCATEGORY.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblCATEGORY.SizeF = New System.Drawing.SizeF(278.7498!, 18.0!)
        Me.lblCATEGORY.StylePriority.UseBorders = False
        Me.lblCATEGORY.StylePriority.UseFont = False
        Me.lblCATEGORY.StylePriority.UseTextAlignment = False
        Me.lblCATEGORY.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.lblCATEGORY.Visible = False
        '
        'XrLabel16
        '
        Me.XrLabel16.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel16.CanShrink = True
        Me.XrLabel16.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "sUntukKeperluan")})
        Me.XrLabel16.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(198.9586!, 249.25!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(551.0414!, 21.125!)
        Me.XrLabel16.StylePriority.UseBorders = False
        Me.XrLabel16.StylePriority.UseFont = False
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel17
        '
        Me.XrLabel17.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel17.CanShrink = True
        Me.XrLabel17.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(0!, 249.25!)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(183.3335!, 21.125!)
        Me.XrLabel17.StylePriority.UseBorders = False
        Me.XrLabel17.StylePriority.UseFont = False
        Me.XrLabel17.StylePriority.UseTextAlignment = False
        Me.XrLabel17.Text = "Catatan"
        Me.XrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel18
        '
        Me.XrLabel18.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel18.CanShrink = True
        Me.XrLabel18.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(183.3335!, 249.25!)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(15.62512!, 21.125!)
        Me.XrLabel18.StylePriority.UseBorders = False
        Me.XrLabel18.StylePriority.UseFont = False
        Me.XrLabel18.StylePriority.UseTextAlignment = False
        Me.XrLabel18.Text = ":"
        Me.XrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'lblTerbilang
        '
        Me.lblTerbilang.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.lblTerbilang.CanShrink = True
        Me.lblTerbilang.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.lblTerbilang.LocationFloat = New DevExpress.Utils.PointFloat(198.9586!, 198.3542!)
        Me.lblTerbilang.Name = "lblTerbilang"
        Me.lblTerbilang.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblTerbilang.SizeF = New System.Drawing.SizeF(551.0414!, 21.125!)
        Me.lblTerbilang.StylePriority.UseBorders = False
        Me.lblTerbilang.StylePriority.UseFont = False
        Me.lblTerbilang.StylePriority.UseTextAlignment = False
        Me.lblTerbilang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel12
        '
        Me.XrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel12.CanShrink = True
        Me.XrLabel12.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(183.3335!, 177.2292!)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(15.62512!, 21.125!)
        Me.XrLabel12.StylePriority.UseBorders = False
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.Text = ":"
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel13
        '
        Me.XrLabel13.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel13.CanShrink = True
        Me.XrLabel13.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel13.LocationFloat = New DevExpress.Utils.PointFloat(0!, 177.2292!)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel13.SizeF = New System.Drawing.SizeF(183.3335!, 21.125!)
        Me.XrLabel13.StylePriority.UseBorders = False
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.StylePriority.UseTextAlignment = False
        Me.XrLabel13.Text = "Uang sejumlah"
        Me.XrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel14
        '
        Me.XrLabel14.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel14.CanShrink = True
        Me.XrLabel14.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "GRANDTOTAL", "{0:'Rp. '#,#}")})
        Me.XrLabel14.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel14.LocationFloat = New DevExpress.Utils.PointFloat(198.9586!, 177.2292!)
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel14.SizeF = New System.Drawing.SizeF(551.0414!, 21.125!)
        Me.XrLabel14.StylePriority.UseBorders = False
        Me.XrLabel14.StylePriority.UseFont = False
        Me.XrLabel14.StylePriority.UseTextAlignment = False
        Me.XrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'lblTelahTerima
        '
        Me.lblTelahTerima.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.lblTelahTerima.CanShrink = True
        Me.lblTelahTerima.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.lblTelahTerima.LocationFloat = New DevExpress.Utils.PointFloat(198.9586!, 156.1042!)
        Me.lblTelahTerima.Name = "lblTelahTerima"
        Me.lblTelahTerima.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblTelahTerima.SizeF = New System.Drawing.SizeF(551.0414!, 21.125!)
        Me.lblTelahTerima.StylePriority.UseBorders = False
        Me.lblTelahTerima.StylePriority.UseFont = False
        Me.lblTelahTerima.StylePriority.UseTextAlignment = False
        Me.lblTelahTerima.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel10
        '
        Me.XrLabel10.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel10.CanShrink = True
        Me.XrLabel10.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(0!, 156.1042!)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(183.3335!, 21.125!)
        Me.XrLabel10.StylePriority.UseBorders = False
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.StylePriority.UseTextAlignment = False
        Me.XrLabel10.Text = "Telah menerima dari"
        Me.XrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel11
        '
        Me.XrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel11.CanShrink = True
        Me.XrLabel11.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(183.3335!, 156.1042!)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(15.62512!, 21.125!)
        Me.XrLabel11.StylePriority.UseBorders = False
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.StylePriority.UseTextAlignment = False
        Me.XrLabel11.Text = ":"
        Me.XrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel7
        '
        Me.XrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel7.CanShrink = True
        Me.XrLabel7.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(183.3335!, 110.5833!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(15.62512!, 21.125!)
        Me.XrLabel7.StylePriority.UseBorders = False
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.Text = ":"
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel5
        '
        Me.XrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel5.CanShrink = True
        Me.XrLabel5.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(183.3335!, 89.45834!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(15.62512!, 21.125!)
        Me.XrLabel5.StylePriority.UseBorders = False
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.Text = ":"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel3
        '
        Me.XrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel3.CanShrink = True
        Me.XrLabel3.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(183.3335!, 68.33334!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(15.62512!, 21.125!)
        Me.XrLabel3.StylePriority.UseBorders = False
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = ":"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel6
        '
        Me.XrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel6.CanShrink = True
        Me.XrLabel6.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(0!, 110.5833!)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(183.3335!, 21.125!)
        Me.XrLabel6.StylePriority.UseBorders = False
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = "BENDAHARA PEMBANTU"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel4
        '
        Me.XrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel4.CanShrink = True
        Me.XrLabel4.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(0!, 89.45834!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(183.3335!, 21.125!)
        Me.XrLabel4.StylePriority.UseBorders = False
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        Me.XrLabel4.Text = "FAKSES"
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel2
        '
        Me.XrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel2.CanShrink = True
        Me.XrLabel2.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(0!, 68.33334!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(183.3335!, 21.125!)
        Me.XrLabel2.StylePriority.UseBorders = False
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "KOTAMA"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'lPHONE
        '
        Me.lPHONE.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.lPHONE.CanShrink = True
        Me.lPHONE.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.lPHONE.LocationFloat = New DevExpress.Utils.PointFloat(198.9586!, 89.45834!)
        Me.lPHONE.Name = "lPHONE"
        Me.lPHONE.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lPHONE.SizeF = New System.Drawing.SizeF(278.7498!, 21.12499!)
        Me.lPHONE.StylePriority.UseBorders = False
        Me.lPHONE.StylePriority.UseFont = False
        Me.lPHONE.StylePriority.UseTextAlignment = False
        Me.lPHONE.Text = "DENKESYAH 03.04.02 GARUT"
        Me.lPHONE.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'lNPWP
        '
        Me.lNPWP.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.lNPWP.CanShrink = True
        Me.lNPWP.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.lNPWP.LocationFloat = New DevExpress.Utils.PointFloat(198.9586!, 110.5833!)
        Me.lNPWP.Name = "lNPWP"
        Me.lNPWP.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lNPWP.SizeF = New System.Drawing.SizeF(278.7496!, 21.125!)
        Me.lNPWP.StylePriority.UseBorders = False
        Me.lNPWP.StylePriority.UseFont = False
        Me.lNPWP.StylePriority.UseTextAlignment = False
        Me.lNPWP.Text = "RUMKIT TK IV 03.07.04/GUNTUR"
        Me.lNPWP.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'lADDRESS
        '
        Me.lADDRESS.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.lADDRESS.CanShrink = True
        Me.lADDRESS.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.lADDRESS.LocationFloat = New DevExpress.Utils.PointFloat(198.9586!, 68.33334!)
        Me.lADDRESS.Name = "lADDRESS"
        Me.lADDRESS.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lADDRESS.SizeF = New System.Drawing.SizeF(278.7498!, 21.125!)
        Me.lADDRESS.StylePriority.UseBorders = False
        Me.lADDRESS.StylePriority.UseFont = False
        Me.lADDRESS.StylePriority.UseTextAlignment = False
        Me.lADDRESS.Text = "KODAM III/SILIWANGI"
        Me.lADDRESS.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel1
        '
        Me.XrLabel1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(489.5832!, 46.16668!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(78.75711!, 22.16666!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "BUKTI NO :"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'sUntukKeperluan
        '
        Me.sUntukKeperluan.Expression = "[MEMO] + ' Tanggal ' + [Parameters.TANGGAL]"
        Me.sUntukKeperluan.Name = "sUntukKeperluan"
        '
        'TANGGAL
        '
        Me.TANGGAL.Description = "Tanggal"
        Me.TANGGAL.Name = "TANGGAL"
        '
        'xtraBuktiPenyetoran
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.sUntukKeperluan})
        Me.DataSource = Me.bindingSource
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(41, 36, 23, 22)
        Me.PageHeight = 583
        Me.PageWidth = 827
        Me.PaperKind = System.Drawing.Printing.PaperKind.A5
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.TANGGAL, Me.YANGMENYETORKAN, Me.YANGMENERIMA, Me.PANGKATYANGMENYETORKAN, Me.PANGKATYANGMENERIMA, Me.TANGGALTANDATANGAN})
        Me.RequestParameters = False
        Me.Version = "15.1"
        CType(Me.bindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents lblREPORT As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents bindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lPHONE As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lNPWP As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lADDRESS As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblTerbilang As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblTelahTerima As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblTanggal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel23 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel24 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel25 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblCATEGORY As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents sUntukKeperluan As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents TANGGAL As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents YANGMENYETORKAN As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents YANGMENERIMA As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents PANGKATYANGMENERIMA As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents PANGKATYANGMENYETORKAN As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents TANGGALTANDATANGAN As DevExpress.XtraReports.Parameters.Parameter
End Class
