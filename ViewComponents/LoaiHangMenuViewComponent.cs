using Microsoft.AspNetCore.Mvc;
using Web_QuanLySieuThiNho.Repo;

namespace Web_QuanLySieuThiNho.ViewComponents
{
    [ViewComponent]
    public class LoaiHangMenuViewComponent : ViewComponent
    {
        private readonly ILoaiHangRepo _loaiHangRepo;
        public LoaiHangMenuViewComponent(ILoaiHangRepo loaiHangRepo) { _loaiHangRepo = loaiHangRepo; }
        public IViewComponentResult Invoke()
        {
            var loaiHangList = _loaiHangRepo.GetAll().OrderBy(x => x.MaLoaiHang);
            return View(loaiHangList);
        }
    }
}
