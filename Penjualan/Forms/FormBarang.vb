Public Class FormBarang

    Dim modeProses As Integer
    Dim baris As Integer

    Private Sub AturButton(st As Boolean)
        BtnTambah.Enabled = st
        BtnUbah.Enabled = st
        BtnHapus.Enabled = st
        BtnSimpan.Enabled = Not st
        BtnBatal.Enabled = Not st

        GroupBox1.Enabled = Not st
        GroupBox3.Enabled = st
        GroupBox4.Enabled = st
    End Sub

    Private Sub IsiBox(br As Integer)
        If br < DTGrid.Rows.Count Then
            With DGBarang.Rows(br)
                TxtKode.Text = .Cells(0).Value.ToString
                TxtNama.Text = .Cells(1).Value.ToString
                TxtHarga.Text = .Cells(2).Value.ToString
                TxtStok.Text = .Cells(3).Value.ToString
            End With
            LblBaris.Text = "Data ke-" & br + 1 & " dari " & DGBarang.RowCount - 1 & " data"
        End If
    End Sub

    Private Sub RefreshGrid()
        DTGrid = KontrolBarang.tampilData.ToTable
        DGBarang.DataSource = DTGrid

        If DTGrid.Rows.Count > 0 Then
            baris = DTGrid.Rows.Count - 1
            DGBarang.Rows(DTGrid.Rows.Count - 1).Selected = True
            DGBarang.CurrentCell = DGBarang.Item(1, baris)
            AturButton(True)
            IsiBox(baris)
        End If
    End Sub

    Private Sub TampilanCari(kunci As String)
        DTGrid = KontrolBarang.cariData(kunci).ToTable

        If DTGrid.Rows.Count > 0 Then
            baris = DTGrid.Rows.Count - 1
            DGBarang.DataSource = DTGrid
            DGBarang.Rows(DTGrid.Rows.Count - 1).Selected = True
            DGBarang.CurrentCell = DGBarang.Item(1, baris)
            IsiBox(baris)
        Else
            MsgBox("Data tidak ditemukan")
            RefreshGrid()
        End If

    End Sub
    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub FormBarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshGrid()
        TxtKode.Enabled = False
    End Sub

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles BtnSimpan.Click
        With Entitasbarang
            .kodeBarang = TxtKode.Text
            .namaBarang = TxtNama.Text
            .hargaBarang = TxtHarga.Text
            .stokBarang = TxtStok.Text

        End With

        If modeProses = 1 Then
            KontrolBarang.InsertData(Entitasbarang)
        ElseIf modeProses = 2 Then
            KontrolBarang.updateData(Entitasbarang)
        End If

        MsgBox("Data Telah Tersimpan", MsgBoxStyle.Information, "info")
        RefreshGrid()

    End Sub

    Private Sub BtnTambah_Click(sender As Object, e As EventArgs) Handles BtnTambah.Click
        AturButton(False)
        modeProses = 1
        TxtNama.Text = ""
        TxtHarga.Text = ""
        TxtStok.Text = ""
        TxtKode.Text = KontrolBarang.kodeBaru()
        TxtNama.Focus()
    End Sub

    Private Sub BtnUbah_Click(sender As Object, e As EventArgs) Handles BtnUbah.Click
        AturButton(False)
        TxtNama.Focus()
        modeProses = 2
    End Sub

    Private Sub BtnBatal_Click(sender As Object, e As EventArgs) Handles BtnBatal.Click
        RefreshGrid()
        AturButton(True)
        modeProses = 0
    End Sub

    Private Sub BtnHapus_Click(sender As Object, e As EventArgs) Handles BtnHapus.Click
        Dim status_referensi As Boolean

        status_referensi = KontrolBarang.cekBarangDireferensi(TxtKode.Text)

        If status_referensi Then
            MsgBox("Data Masih Digunakan, Tidak Boleh Dihapus", MsgBoxStyle.Exclamation, "Peringatan")
            Exit Sub
        End If

        If MsgBox("Apakah Anda Yakin Menghapus " & TxtKode.Text & "_" & TxtNama.Text & " ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Konfirmasi") = MsgBoxResult.Yes Then
            KontrolBarang.deleteData(TxtKode.Text)
            Exit Sub
        End If

        RefreshGrid()
    End Sub

    Private Sub BtnSelesai_Click(sender As Object, e As EventArgs) Handles BtnSelesai.Click
        Me.Close()
    End Sub

    Private Sub DGBarang_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGBarang.CellContentClick
        If modeProses = 0 Then
            baris = e.RowIndex
            DGBarang.Rows(baris).Selected = True
            IsiBox(baris)
        End If
    End Sub

    Private Sub BtnAwal_Click(sender As Object, e As EventArgs) Handles BtnAwal.Click
        DGBarang.ClearSelection()
        baris = 0
        DGBarang.Rows(baris).Selected = True
        IsiBox(baris)

    End Sub

    Private Sub BtnAkhir_Click(sender As Object, e As EventArgs) Handles BtnAkhir.Click
        DGBarang.ClearSelection()
        baris = DTGrid.Rows.Count - 1
        DGBarang.Rows(baris).Selected = True
        IsiBox(baris)

    End Sub

    Private Sub BtnNaik_Click(sender As Object, e As EventArgs) Handles BtnNaik.Click
        DGBarang.ClearSelection()
        If baris < DTGrid.Rows.Count - 1 Then baris = baris + 1
        DGBarang.Rows(baris).Selected = True
        IsiBox(baris)
    End Sub

    Private Sub BtnTurun_Click(sender As Object, e As EventArgs) Handles BtnTurun.Click
        DGBarang.ClearSelection()
        If baris > 0 Then baris = baris - 1
        DGBarang.Rows(baris).Selected = True
        IsiBox(baris)
    End Sub

    Private Sub BtnCari_Click(sender As Object, e As EventArgs) Handles BtnCari.Click
        If TxtCari.Text = "" Then
            Call RefreshGrid()
        Else
            Call TampilanCari(TxtCari.Text)
            TxtCari.Focus()
        End If
    End Sub
End Class
