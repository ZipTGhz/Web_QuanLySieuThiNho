using Microsoft.EntityFrameworkCore;
using Web_QuanLySieuThiNho.Models;

namespace Web_QuanLySieuThiNho.Repo
{
    public class LoaiHangRepo : ILoaiHangRepo
    {
        private QlsieuThiNhoContext _context;
        public LoaiHangRepo(QlsieuThiNhoContext context) { _context = context; }
        public TLoaiHang Add(TLoaiHang item)
        {
            _context.TLoaiHangs.Add(item);
            _context.SaveChanges();
            return item;
        }

        public TLoaiHang Delete(string maLoaiHang)
        {
            return null;
        }

        public IEnumerable<TLoaiHang> GetAll()
        {
            return _context.TLoaiHangs;
        }

        public TLoaiHang GetLoaiHang(string maLoaiHang)
        {
            return _context.TLoaiHangs.Find(maLoaiHang);
        }

        public TLoaiHang Update(TLoaiHang item)
        {
            if (item == null)
                    return null;
            _context.Update(item);
            _context.SaveChanges();
            return item;
        }
    }
}
