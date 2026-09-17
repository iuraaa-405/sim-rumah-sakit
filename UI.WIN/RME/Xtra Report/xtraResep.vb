Imports DataAccess

Public Class xtraResep
    Private Sub xtraCashIn_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Try
            If sPictureLogo IsNot Nothing Then
                XrPictureBox1.Image = sPictureLogo
            End If
            lbl1.Text = sCompany
            lbl1.Text = sAddress

            Dim value1 As Object = GetCurrentColumnValue("KODETTD")
            Dim value2 As Object = GetCurrentColumnValue("PETUGASFARMASI")

            If value1 IsNot Nothing Then
                Dim Alamat As String = sALAMATTTD & value1.ToString() & ".jpg"
                If FileIO.FileSystem.FileExists(Alamat) Then
                    picDokter.Image = Image.FromFile(Alamat)
                End If
            End If
            If value2 IsNot Nothing Then
                Dim Alamat As String = sALAMATTTD & value2.ToString() & ".jpg"
                If FileIO.FileSystem.FileExists(Alamat) Then
                    picPetugasFarmasi.Image = Image.FromFile(Alamat)
                End If
            End If
        Catch ex As Exception
            'MsgBox("Alamat Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        End Try

        'Dim oItem As New Reference.clsItem
        'Dim oDaftarL4 As New Reference.clsDaftar_L4
        'Dim oRME As New RME.clsRME
        'Dim listPlanning As New List(Of String)
        'Dim listTindakanLab As New List(Of String)
        'Dim listTindakanRad As New List(Of String)
        'Dim listTindakanLainnya As New List(Of String)
        'Dim listTindakanTerapi As New List(Of String)

        'Dim sKDCPPT As String = String.Empty

        'Dim value1 As Object = GetCurrentColumnValue("KDCPPT")

        'If value1 IsNot Nothing Then
        '    sKDCPPT = value1.ToString()

        '    Dim oGrouperDataCppt As New Grouper.clsR_CPPT

        '    For Each xloop In oGrouperDataCppt.GetDataDetailTindakan(sKDCPPT)
        '        Dim dsItem = oItem.GetData(xloop.KDITEM)
        '        If dsItem IsNot Nothing Then
        '            If dsItem.M_ITEM_L3.MEMO = "LABORATORIUM" Then
        '                listTindakanLab.Add(dsItem.NMITEM2 & " " & xloop.MEMO)
        '            ElseIf dsItem.M_ITEM_L3.MEMO = "RADIOLOGI" Then
        '                listTindakanRad.Add(dsItem.NMITEM2 & " " & xloop.MEMO)
        '            Else
        '                listTindakanLainnya.Add(dsItem.NMITEM2 & " " & xloop.MEMO)
        '            End If
        '        End If
        '    Next

        '    If listTindakanLab.Count > 0 Then
        '        listPlanning.Add(String.Join(", ", listTindakanLab.ToArray))
        '    End If
        '    If listTindakanRad.Count > 0 Then
        '        listPlanning.Add(String.Join(", ", listTindakanRad.ToArray))
        '    End If
        '    'If listTindakanLainnya.Count > 0 Then
        '    '    listPlanning.Add("TINDAKAN LAIN" & vbCrLf & String.Join(vbCrLf, listTindakanLainnya.ToArray))
        '    'End If

        '    For Each xloop In oGrouperDataCppt.GetDataDetailNonRacikan(sKDCPPT)
        '        listTindakanTerapi.Add(xloop.NAMAOBAT & " " & xloop.SATUAN & " " & xloop.SIGNA & " " & xloop.CARAPAKAI & " No " & oRME.IntegerToRoman(xloop.JUMLAH) & " " & xloop.REMARKS_DOKTER)
        '    Next

        '    For Each xloop In oGrouperDataCppt.GetDataDetailRacikan(sKDCPPT)
        '        listTindakanTerapi.Add(xloop.SEQ + 1 & ". " & xloop.NAMAOBAT & " " & xloop.SATUAN & " " & xloop.SIGNA & " " & xloop.PERMINTAAN & " " & xloop.REMARKS)
        '    Next

        '    If listTindakanTerapi.Count > 0 Then
        '        lblTerapi.Text = String.Join(", ", listTindakanTerapi.ToArray)
        '    End If

        '    Dim dsCPPT = oGrouperDataCppt.GetData(sKDCPPT)
        '    If dsCPPT IsNot Nothing Then
        '        'If dsCPPT.R_IDENTITAS_GROUPER.dpjpkodevclaim <> "" Then
        '        '    XrPictureBox1.Image = CType(My.Resources.ResourceManager.GetObject(dsCPPT.R_IDENTITAS_GROUPER.dpjpkodevclaim), Image)
        '        'End If

        '        If dsCPPT.OBJEKTIF_PEMERIKSAAN <> "" Then
        '            listPlanning.Add(dsCPPT.OBJEKTIF_PEMERIKSAAN)
        '        End If

        '        'lblUmur.Text = oRME.GetUmurPasien(dsCPPT.DATE, dsCPPT.R_IDENTITAS_GROUPER.tgl_lahir)

        '        Try
        '            Dim oDoctor As New Reference.clsDoctor

        '            Dim dsDoctor = oDoctor.GetData(dsCPPT.A_IDENTITASPASIEN_LIST.KDDOCTOR)
        '            If dsDoctor IsNot Nothing Then
        '                If dsDoctor.KODETTD <> "" Then
        '                    Dim Alamat As String = sALAMATTTD & dsDoctor.KODETTD & ".jpg"
        '                    If FileIO.FileSystem.FileExists(Alamat) Then
        '                        XrPictureBox1.Image = Image.FromFile(Alamat)
        '                    End If
        '                End If
        '            End If
        '        Catch ex As Exception
        '            'MsgBox("Alamat Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        '        End Try
        '    End If

        '    If listPlanning.Count > 0 Then
        '        lblPenunjang.Text = String.Join(vbCrLf, listPlanning.ToArray)
        '    End If

        '    Dim dsDaftar_L4 = oDaftarL4.GetData(dsCPPT.PLANNING_ISTINDAKLANJUT_KONSUL_TEXT)
        '    If dsDaftar_L4 IsNot Nothing Then
        '        lblTindakLanjut.Text = dsDaftar_L4.MEMO & " " & dsCPPT.PLANNING_ISTINDAKLANJUT_RUJUK_TEXT
        '    Else
        '        lblTindakLanjut.Text = dsCPPT.PLANNING_ISTINDAKLANJUT_RUJUK_TEXT
        '    End If

        ' End If
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
End Class