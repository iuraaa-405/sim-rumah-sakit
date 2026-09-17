Imports DataAccess

Public Class xtraDigital_CPPT_01_QR
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        If sPictureLogo IsNot Nothing Then
            XrPictureBox1.Image = sPictureLogo
        End If

        txtNORM.Text = sFind1_cppt
        txtNAMA.Text = sFind2_cppt
        txtTGLLAHIR.Text = sFind3_cppt
    End Sub
    Private Sub Detail_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Detail.BeforePrint
        Dim sKDPROFESI As String = String.Empty

        Dim value1 As Object = GetCurrentColumnValue("KDPROFESI")
        Dim valueAlergi As Object = GetCurrentColumnValue("SUBJEKTIF_ALERGI_YA")

        If value1 IsNot Nothing Then
            sKDPROFESI = value1.ToString()
        End If

        If valueAlergi IsNot Nothing Then
            If CBool(valueAlergi.ToString()) = True Then
                lblAlergi1.Visible = True
                lblAlergi2.Visible = True
                lblAlergi3.Visible = True
            Else
                lblAlergi1.Visible = False
                lblAlergi2.Visible = False
                lblAlergi3.Visible = False
            End If
        End If

        Dim oProfesi As New Reference.clsProfesi

        Dim dsProfesi = oProfesi.GetData(sKDPROFESI)

        If dsProfesi IsNot Nothing Then
            If dsProfesi.MEMO.ToString.Contains("DOKTER") Then
                XrTableCell58.ForeColor = Color.Black
                XrTableCell20.ForeColor = Color.Black
                XrTableCell21.ForeColor = Color.Black
                XrTableCell22.ForeColor = Color.Black
                XrTableCell27.ForeColor = Color.Black
                XrTableCell30.ForeColor = Color.Black
                XrTableCell31.ForeColor = Color.Black
                XrTableCell23.ForeColor = Color.Black
                XrTableCell19.ForeColor = Color.Black
                XrTableCell36.ForeColor = Color.Black
                XrTableCell77.ForeColor = Color.Black
                XrTableCell78.ForeColor = Color.Black
                XrTableCell79.ForeColor = Color.Black
                XrTableCell17.ForeColor = Color.Black
                XrTableCell56.ForeColor = Color.Black
                XrTableCell57.ForeColor = Color.Black
                XrTableCell59.ForeColor = Color.Black
                XrTableCell39.ForeColor = Color.Black
                XrTableCell33.ForeColor = Color.Black
            Else
                XrTableCell58.ForeColor = Color.Blue
                XrTableCell20.ForeColor = Color.Blue
                XrTableCell21.ForeColor = Color.Blue
                XrTableCell22.ForeColor = Color.Blue
                XrTableCell27.ForeColor = Color.Blue
                XrTableCell30.ForeColor = Color.Blue
                XrTableCell31.ForeColor = Color.Blue
                XrTableCell23.ForeColor = Color.Blue
                XrTableCell19.ForeColor = Color.Blue
                XrTableCell36.ForeColor = Color.Blue
                XrTableCell77.ForeColor = Color.Blue
                XrTableCell78.ForeColor = Color.Blue
                XrTableCell79.ForeColor = Color.Blue
                XrTableCell17.ForeColor = Color.Blue
                XrTableCell56.ForeColor = Color.Blue
                XrTableCell57.ForeColor = Color.Blue
                XrTableCell59.ForeColor = Color.Blue
                XrTableCell39.ForeColor = Color.Blue
                XrTableCell33.ForeColor = Color.Blue
            End If
        Else
            If sKDPROFESI.Contains("DOKTER") Then
                XrTableCell58.ForeColor = Color.Black
                XrTableCell20.ForeColor = Color.Black
                XrTableCell21.ForeColor = Color.Black
                XrTableCell22.ForeColor = Color.Black
                XrTableCell27.ForeColor = Color.Black
                XrTableCell30.ForeColor = Color.Black
                XrTableCell31.ForeColor = Color.Black
                XrTableCell23.ForeColor = Color.Black
                XrTableCell19.ForeColor = Color.Black
                XrTableCell36.ForeColor = Color.Black
                XrTableCell77.ForeColor = Color.Black
                XrTableCell78.ForeColor = Color.Black
                XrTableCell79.ForeColor = Color.Black
                XrTableCell17.ForeColor = Color.Black
                XrTableCell56.ForeColor = Color.Black
                XrTableCell57.ForeColor = Color.Black
                XrTableCell59.ForeColor = Color.Black
                XrTableCell39.ForeColor = Color.Black
                XrTableCell33.ForeColor = Color.Black
            Else
                XrTableCell58.ForeColor = Color.Blue
                XrTableCell20.ForeColor = Color.Blue
                XrTableCell21.ForeColor = Color.Blue
                XrTableCell22.ForeColor = Color.Blue
                XrTableCell27.ForeColor = Color.Blue
                XrTableCell30.ForeColor = Color.Blue
                XrTableCell31.ForeColor = Color.Blue
                XrTableCell23.ForeColor = Color.Blue
                XrTableCell19.ForeColor = Color.Blue
                XrTableCell36.ForeColor = Color.Blue
                XrTableCell77.ForeColor = Color.Blue
                XrTableCell78.ForeColor = Color.Blue
                XrTableCell79.ForeColor = Color.Blue
                XrTableCell17.ForeColor = Color.Blue
                XrTableCell56.ForeColor = Color.Blue
                XrTableCell57.ForeColor = Color.Blue
                XrTableCell59.ForeColor = Color.Blue
                XrTableCell39.ForeColor = Color.Blue
                XrTableCell33.ForeColor = Color.Blue
            End If
        End If
    End Sub
End Class