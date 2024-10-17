using Web_QuanLySieuThiNho.Models;

namespace Web_QuanLySieuThiNho.Repo
{
    public interface ILoaiHangRepo
    {
        TLoaiHang Add(TLoaiHang item);
        TLoaiHang Update(TLoaiHang item);
        TLoaiHang Delete(string maLoaiHang);
        TLoaiHang GetLoaiHang(string maLoaiHang);
        IEnumerable<TLoaiHang> GetAll();

    }
}
