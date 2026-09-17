Imports System.Linq
Imports QRCoder
Imports DataAccess

Public Class xtraReportEMedrekRJ_24
    Private Sub picPasien_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picPasien.BeforePrint 
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganPasien, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picPasien.Image = code.GetGraphic(6)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub picPerawat_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picPerawat.BeforePrint 
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganUser, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picPerawat.Image = code.GetGraphic(6)
        Catch ex As Exception

        End Try
    End Sub

    Dim oDiagnosa As New Digital.clsDigital_RJ_24
    Private Sub cellDiagnosa_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles cellDiagnosa.BeforePrint
        Dim sNOIDASKEP As String 
        Dim sKdItemDiagnosa As String 

        Try
            sNOIDASKEP = Report.GetCurrentColumnValue("KDKUNJUNGAN")
            sKdItemDiagnosa = DetailReport.GetCurrentColumnValue("KDITEMDIAGNOSAPERAWAT")
            Dim listHasil As New List(Of String)
            Dim listHasil2 As New List(Of String)
            Dim dsDiagnosa = oDiagnosa.GetData(sNOIDASKEP)
            Dim strdiagnosa As String = ""
            Dim count As String = ""

            If dsDiagnosa IsNot Nothing Then
                For Each xloop In oDiagnosa.GetDataDetail(sNOIDASKEP).Where(Function(x) (x.DIAGNOSA_KEPERAWATAN.Contains(":") Or x.CEK1 = True) And x.KDITEMDIAGNOSAPERAWAT=sKdItemDiagnosa)
                    If xloop.DIAGNOSA_KEPERAWATAN.Contains("b.d") Or xloop.DIAGNOSA_KEPERAWATAN.Contains("dibuktikan dengan") Then
                        listHasil.Add(xloop.DIAGNOSA_KEPERAWATAN)
                    ElseIf xloop.DIAGNOSA_KEPERAWATAN.Contains(":") Then
                        listHasil.Add(vbCrLf & xloop.DIAGNOSA_KEPERAWATAN)
                    Else
                        listHasil.Add(xloop.DIAGNOSA_KEPERAWATAN)
                    End If
                Next

                strdiagnosa = strdiagnosa & String.Join(vbCrLf, listHasil.ToArray)
                cellDiagnosa.Text = strdiagnosa
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cellLuaran_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles cellLuaran.BeforePrint
        Dim sNOIDASKEP As String 
        Dim sKdItemDiagnosa As String 

        Try
            sNOIDASKEP = Report.GetCurrentColumnValue("KDKUNJUNGAN")
            sKdItemDiagnosa = DetailReport.GetCurrentColumnValue("KDITEMDIAGNOSAPERAWAT")
            Dim listHasil As New List(Of String)
            Dim listHasil2 As New List(Of String)
            Dim dsDiagnosa = oDiagnosa.GetData(sNOIDASKEP)
            Dim strdiagnosa As String = ""
            Dim count As String = ""

            If dsDiagnosa IsNot Nothing Then
                For Each xloop In oDiagnosa.GetDataDetail(sNOIDASKEP).Where(Function(x) (x.LUARAN.Contains("Setelah dilakukan perawatan dalam waktu") Or x.LUARAN.Contains(":") Or x.CEK2 = True) And x.KDITEMDIAGNOSAPERAWAT=sKdItemDiagnosa)
                    If xloop.LUARAN.Contains("Setelah dilakukan perawatan dalam waktu") Then
                        listHasil.Add(xloop.LUARAN)
                    ElseIf xloop.LUARAN.Contains(":") Then
                        listHasil.Add(vbCrLf & xloop.LUARAN)
                    Else
                        listHasil.Add(xloop.LUARAN)
                    End If
                Next

                strdiagnosa = strdiagnosa & String.Join(vbCrLf, listHasil.ToArray)
                cellLuaran.Text = strdiagnosa
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub celIntervensi_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles celIntervensi.BeforePrint
        Dim sNOIDASKEP As String 
        Dim sKdItemDiagnosa As String 

        Try
            sNOIDASKEP = Report.GetCurrentColumnValue("KDKUNJUNGAN")
            sKdItemDiagnosa = DetailReport.GetCurrentColumnValue("KDITEMDIAGNOSAPERAWAT")
            Dim listHasil As New List(Of String)
            Dim listHasil2 As New List(Of String)
            Dim dsDiagnosa = oDiagnosa.GetData(sNOIDASKEP)
            Dim strdiagnosa As String = ""
            Dim count As String = ""

            If dsDiagnosa IsNot Nothing Then
                For Each xloop In oDiagnosa.GetDataDetail(sNOIDASKEP).Where(Function(x) (x.INTERVENSI.Contains(":") Or x.CEK3 = True) And x.KDITEMDIAGNOSAPERAWAT=sKdItemDiagnosa)
                    listHasil.Add(xloop.INTERVENSI)
                Next

                strdiagnosa = strdiagnosa & String.Join(vbCrLf, listHasil.ToArray)
                celIntervensi.Text = strdiagnosa
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class