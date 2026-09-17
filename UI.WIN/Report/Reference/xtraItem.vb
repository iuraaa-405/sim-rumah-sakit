Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class xtraItem

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        fn_LoadLanguage()
    End Sub

    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Item.TITLE_1.ToUpper
            lTITLE.Text = Item.TITLE_1.ToUpper

            chkISACTIVE.Text = Item.ISACTIVE.ToUpper

            lNMITEM1.Text = Item.NMITEM1_2.ToUpper
            lNMITEM2.Text = Item.NMITEM2_2.ToUpper
            lNMITEM3.Text = Item.NMITEM3_2.ToUpper

            lKDITEM_L1.Text = Item_L1.TITLE.ToUpper
            lKDITEM_L2.Text = Item_L2.TITLE.ToUpper
            lKDITEM_L3.Text = Item_L3.TITLE.ToUpper
            lKDITEM_L5.Text = Item_L4.TITLE.ToUpper
            lKDITEM_L4.Text = Item_L5.TITLE.ToUpper
            lKDITEM_L6.Text = Item_L6.TITLE.ToUpper

            lKDCOA_COST.Text = Item.KDCOA_COST.ToUpper
            lKDCOA_INVENTORY.Text = Item.KDCOA_INVENTORY.ToUpper
            lKDCOA_SALES.Text = Item.KDCOA_SALES.ToUpper

            lINFORMATION_SPECIFICATION.Text = Item.TAB_SPESIFICATION.ToUpper
            lINFORMATION_ACCOUNT.Text = Item.TAB_ACCOUNT.ToUpper
            lINFORMATION_UNIT.Text = Item.TAB_SATUAN.ToUpper

            lDETAILUOM_KDUOM.Text = Item.DETAILUOM_KDUOM.ToUpper
            lDETAILUOM_RATE.Text = Item.DETAILUOM_RATE.ToUpper
            lDETAILUOM_PRICEPURCHASESTANDARD.Text = Item.DETAILUOM_PRICEPURCHASESTANDARD.ToUpper
            lDETAILUOM_PRICESALESSTANDARD.Text = Item.DETAILUOM_PRICESALESSTANDARD.ToUpper
            lDETAILUOM_MARGIN.Text = Item.DETAILUOM_MARGIN.ToUpper
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    Private Function ImageToByteArray(ByVal ImageIn As System.Drawing.Image) As Byte()
        Using ms As New System.IO.MemoryStream
            ImageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif)
            Return ms.ToArray
        End Using
    End Function

    Private Sub picATTACHMENT_BeforePrint(sender As System.Object, e As System.Drawing.Printing.PrintEventArgs) Handles picATTACHMENT.BeforePrint
        Dim oItem As New DataAccess.Reference.clsItem

        Try
            Dim img = (From x In oItem.GetData _
                  Where x.KDITEM = Me.GetCurrentColumnValue("KDITEM") _
                  Select x.ATTACHMENT).Single

            picATTACHMENT.Image = ByteArrayToImage(img.ToArray())
        Catch oErr As Exception

        End Try
    End Sub
End Class