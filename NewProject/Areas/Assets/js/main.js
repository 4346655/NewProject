//add hovered class to selected list item

const btnheader__totggle = document.querySelector('.header__totggle');
const navigation_list = document.querySelector('.navigation');
const mainn = document.querySelector('.main');

btnheader__totggle.addEventListener("click",() =>{
    navigation_list.classList.toggle("active");
    // có thì bỏ, ko có thì thêm
    if(navigation_list.classList.contains('active')){// kiểm tra actice có trong chưa
      mainn.setAttribute("style","left: 300px;width: calc(100% - 300px)");
      return
    }
    mainn.setAttribute("style","left: 90px");
})

