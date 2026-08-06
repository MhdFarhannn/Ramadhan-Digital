# Panduan Penggunaan Fitur Registrasi Bulk via Excel

## 📋 Daftar Isi
1. [Persiapan File Excel](#persiapan-file-excel)
2. [Struktur Data](#struktur-data)
3. [Cara Menggunakan API](#cara-menggunakan-api)
4. [Contoh Response](#contoh-response)
5. [Troubleshooting](#troubleshooting)

---

## Persiapan File Excel

### Format Kolom
Buatlah file Excel (.xlsx atau .xls) dengan struktur kolom berikut:

| Kolom | Header | Tipe | Wajib | Keterangan |
|-------|--------|------|-------|-----------|
| A | Nama | Text | ✓ | Nama lengkap user |
| B | Username | Text | ✓ | Username unik untuk login |
| C | Password | Text | ✓ | Password awal (akan di-hash otomatis) |
| D | IdRole | Number | ✓ | 1=Admin, 2=Guru, 3=Santri |
| E | IdKelas | Number | ✗ | ID Kelas (bisa kosong) |

### Contoh Data Excel

```
Nama                  | Username      | Password      | IdRole | IdKelas
============================================================================
Ahmad Bin Abdillah    | ahmad123      | qwerty123     | 3      | 1
Siti Nurhaliza        | siti456       | password123   | 3      | 1
Muhammad Ilham        | ilham789      | ilham@2024    | 2      | 
Fatima Al-Zahra       | fatima001     | fatima.pwd    | 3      | 2
```

---

## Struktur Data

### IdRole Values
- **1** = Admin (Administrator)
- **2** = Guru (Pengajar)
- **3** = Santri (Pelajar/Murid)

### IdKelas Values
- Gunakan ID kelas yang sudah terdaftar di sistem
- Bersifat opsional (bisa dikosongkan)
- Jika dikosongkan, user tidak akan terikat ke kelas tertentu

### Validasi Data
- **Username**: Harus unik, jika sudah ada akan dilewatkan
- **Password**: Otomatis di-hash dengan BCrypt
- **Nama**: Maksimal 255 karakter
- **IdRole**: Harus valid (1, 2, atau 3)

---

## Cara Menggunakan API

### Endpoint
```
POST /api/v1/auth/register-bulk-excel
```

### Authentication
Memerlukan token JWT dengan role **Admin**

```
Authorization: Bearer {YOUR_JWT_TOKEN}
```

### Request Format
Kirim sebagai **multipart/form-data**:

```bash
curl -X POST "https://api.example.com/api/v1/auth/register-bulk-excel" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -F "file=@users.xlsx"
```

### Content-Type
File harus berformat:
- `.xlsx` (Excel 2007+)
- `.xls` (Excel 97-2003)

---

## Contoh Response

### Sukses (Status 200 OK)
```json
{
  "success": true,
  "importedCount": 4,
  "errors": [],
  "message": "Berhasil mengimpor 4 user"
}
```

### Sukses Sebagian (Status 400 Bad Request)
```json
{
  "success": false,
  "importedCount": 2,
  "errors": [
	"Baris 3: Username 'ahmad123' sudah terdaftar",
	"Error menyimpan user 'siti456': Database connection error"
  ],
  "message": "Gagal mengimpor data dari Excel"
}
```

### Gagal - File Kosong
```json
{
  "success": false,
  "importedCount": 0,
  "errors": [],
  "message": "File tidak ditemukan atau kosong"
}
```

### Gagal - Format File Salah
```json
{
  "success": false,
  "importedCount": 0,
  "errors": [],
  "message": "Format file harus .xlsx atau .xls"
}
```

---

## Troubleshooting

### ❌ Error: "Username '{username}' sudah terdaftar"
**Penyebab**: Username sudah ada di database
**Solusi**: 
- Gunakan username yang berbeda
- Atau update username yang ada melalui endpoint lain

### ❌ Error: "Kolom 'Nama' tidak boleh kosong"
**Penyebab**: Kolom Nama pada baris tertentu kosong
**Solusi**: 
- Pastikan setiap baris memiliki Nama
- Periksa kolom A pada Excel

### ❌ Error: "Kolom 'IdRole' harus berupa angka"
**Penyebab**: IdRole tidak berupa angka atau berformat teks
**Solusi**:
- Ubah format kolom D ke "Number"
- Gunakan nilai: 1, 2, atau 3

### ❌ Error: "Format file harus .xlsx atau .xls"
**Penyebab**: File bukan format Excel
**Solusi**:
- Pastikan file berakhir dengan .xlsx atau .xls
- Jangan menggunakan format CSV atau format lain

### ❌ Error: "File Excel kosong atau hanya memiliki header"
**Penyebab**: Tidak ada data di Excel selain header
**Solusi**:
- Tambahkan minimal 1 baris data
- Pastikan data dimulai dari baris ke-2

### ❌ Unauthorized (Status 401)
**Penyebab**: Token tidak valid atau expired
**Solusi**:
- Login kembali untuk mendapatkan token baru
- Pastikan token dikirim di header Authorization

### ❌ Forbidden (Status 403)
**Penyebab**: User tidak memiliki role Admin
**Solusi**:
- Gunakan akun Admin untuk import
- Minta admin untuk melakukan import

---

## Best Practices

✅ **Lakukan**:
1. Validasi data di Excel sebelum import
2. Buat backup data sebelum import
3. Gunakan password yang kuat untuk pengguna baru
4. Cek error report setelah import
5. Gunakan Excel 2007+ format (.xlsx)

❌ **Jangan Lakukan**:
1. Import data dengan username duplikat tanpa pengecekan
2. Menggunakan password yang mudah ditebak
3. Mengubah struktur kolom Excel
4. Mengupload file dengan ukuran terlalu besar
5. Import dari source yang tidak terpercaya

---

## Contoh Implementasi di Frontend

### JavaScript/TypeScript dengan Fetch

```javascript
async function importUsersFromExcel(file, token) {
  const formData = new FormData();
  formData.append('file', file);

  try {
	const response = await fetch(
	  'https://api.example.com/api/v1/auth/register-bulk-excel',
	  {
		method: 'POST',
		headers: {
		  'Authorization': `Bearer ${token}`
		},
		body: formData
	  }
	);

	const data = await response.json();

	if (data.success) {
	  console.log(`✓ Berhasil import ${data.importedCount} user`);
	} else {
	  console.error('Import gagal:');
	  data.errors.forEach(error => console.error(`  - ${error}`));
	}

	return data;
  } catch (error) {
	console.error('Error:', error);
  }
}

// Usage
const fileInput = document.getElementById('file-input');
const token = localStorage.getItem('authToken');
const result = await importUsersFromExcel(fileInput.files[0], token);
```

---

## Limit & Batasan

- **Ukuran file maksimal**: 10 MB
- **Jumlah baris maksimal**: 10,000 per file
- **Karakter maksimal per cell**: 32,767
- **Timeout**: 5 menit per request

---

## Support

Jika mengalami masalah:
1. Cek dokumentasi ini terlebih dahulu
2. Verifikasi format file Excel
3. Pastikan token masih valid
4. Hubungi administrator sistem
