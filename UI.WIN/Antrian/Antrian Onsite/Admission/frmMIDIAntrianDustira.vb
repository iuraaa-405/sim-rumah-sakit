Public Class frmMIDIAntrianDustira
#Region "Declaration"

#End Region
#Region "Function"
    Private sKategori As Boolean = False

    Public Sub fn_LoadMeAuto(ByVal Kategori As Boolean)
        sKategori = Kategori
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Timer1.Start()
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        Dim hari = ""
        Select Case Now.ToString("dddd")
            Case "Sunday"
                hari = "MINGGU"
            Case "Monday"
                hari = "SENIN"
            Case "Tuesday"
                hari = "SELASA"
            Case "Wednesday"
                hari = "RABU"
            Case "Thursday"
                hari = "KAMIS"
            Case "Friday"
                hari = "JUMAT"
            Case "Saturday"
                hari = "SABTU"
            Case Else
                hari = "-"
        End Select
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        fn_ChangeFormState()
    End Sub
    Private Sub picAntrianSKDOnline_Click(sender As Object, e As EventArgs) Handles picAntrianSKDOnline.Click
        If sKategori = False Then
            Dim frmAntrianSKDOnline As New frmAntrianSKDOnline
            Try
                frmAntrianSKDOnline.fn_LoadMeAuto(sKategori)
                frmAntrianSKDOnline.WindowState = FormWindowState.Normal
                frmAntrianSKDOnline.ShowDialog()
            Catch oErr As Exception
                MsgBox("Load Form" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmAntrianSKDOnline Is Nothing Then frmAntrianSKDOnline.Dispose()
                frmAntrianSKDOnline = Nothing
            End Try
        Else
            Dim frmAntrianOnline As New frmAntrianOnline
            Try
                frmAntrianOnline.WindowState = FormWindowState.Normal
                frmAntrianOnline.ShowDialog()
            Catch oErr As Exception
                MsgBox("Load Form" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmAntrianOnline Is Nothing Then frmAntrianOnline.Dispose()
                frmAntrianOnline = Nothing
            End Try
        End If
    End Sub
    Private Sub picAntrianManual_Click(sender As Object, e As EventArgs) Handles picAntrianManual.Click
        'Dim frmIdentifikasiPasienNew As New frmIdentifikasiPasienNew
        'Try
        '    frmIdentifikasiPasienNew.WindowState = FormWindowState.Normal
        '    frmIdentifikasiPasienNew.ShowDialog()

        '    If sKDCUSTOMER_ANTRIAN <> "" Then
        '        Dim frmAntrianOflineNewPoli As New frmAntrianOflineNewPoli

        '        frmAntrianOflineNewPoli.WindowState = FormWindowState.Maximized
        '        frmAntrianOflineNewPoli.ShowDialog()
        '    End If

        'Catch oErr As Exception
        '    MsgBox("Load Form" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmAntrianSKDOnline Is Nothing Then frmAntrianSKDOnline.Dispose()
        '    frmAntrianSKDOnline = Nothing
        'End Try
        'Dim frmAntrianOflineNew As New frmAntrianOflineNew
        'Try
        '    frmAntrianOflineNew.WindowState = FormWindowState.Normal
        '    frmAntrianOflineNew.ShowDialog()

        '    If sKDCUSTOMER_ANTRIAN <> "" Then
        '        Dim frmAntrianOflineNewPoli As New frmAntrianOflineNewPoli

        '        frmAntrianOflineNewPoli.WindowState = FormWindowState.Maximized
        '        frmAntrianOflineNewPoli.ShowDialog()
        '    End If

        'Catch oErr As Exception
        '    MsgBox("Load Form" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmAntrianOflineNew Is Nothing Then frmAntrianOflineNew.Dispose()
        '    frmAntrianOflineNew = Nothing
        'End Try
        Dim frmAntrianOnsiteKuota As New frmAntrianOnsiteKuota
        Try
            frmAntrianOnsiteKuota.ShowDialog()
        Catch oErr As Exception
            MsgBox("Load Form" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmAntrianOnsiteKuota Is Nothing Then frmAntrianOnsiteKuota.Dispose()
            frmAntrianOnsiteKuota = Nothing
        End Try
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs)
        Timer1.Stop()
        Me.Close()
    End Sub
    Private Sub SimpleButton1_Click_1(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Timer1.Stop()
        Me.Close()
    End Sub
#End Region
End Class