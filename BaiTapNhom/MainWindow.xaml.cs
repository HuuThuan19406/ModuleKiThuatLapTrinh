using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace BaiTapNhom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection con;//Khai báo đối tượng thực hiện kết nối đến cơ sở dữ liệu
        SqlCommand cmd;//Khai báo đối tượng thực hiện các câu lệnh truy vấn
        string urlAvatar, query;
        string base64_defaultAvatar = "iVBORw0KGgoAAAANSUhEUgAAAP8AAAFBCAYAAAC4v3l7AAAACXBIWXMAAC4jAAAuIwF4pT92AAAgAElEQVR4nO2dfXBV5Z3Hn5ub9xcSAoSQQHiTVh0rqLWr1mwYHae6tZFottudTivddrZ2amtcO0oqHXWGHWf2jxan1vaPbcX/1i27op1t+08taah2V4UgaEXeQSBACElISAi5uTu/wznh5Nzzfp7nvD3fz8ytTXK59znn3u/v7fk9z5PJ5/MMpI/W1tZljDH9o44xtkZ3oYsYY59ijJ1njI0wxsoZY2WMsTHG2BRjbIIxtpgxVqX7Nz2GG3VEfRDb6X96e3u34+uUDCD+hNPa2rpGFTUJfK0q8tURX9UwY6xP/+jt7e1L/M1OGRB/gmhtbV2rCl17RC1yr/SoEQIZg23JGnr6gPhjihq2a2Jfm0Chu4GMARmB7YgMwgfijwk6sWuPpZLdgqOqIdgCQxAOEH+EqPn6OvWRRs/uFxiCEID4Q6a1tZUKcuvVBwTvzG7G2GYyBr29vUNxH2ySgPhDQhV9l/qoleKi+TKsRgObEQ3wAeIXDEQvhB7VCGDGIAAQv0BaW1ufheiFQrWBZ3t7e7ek+BqFAfELQJ2P3yJhxT4qYAR8APFzRA3x6Qv4QGouKlnACHgA4ueE6u23IcSPBWQEulATsAfi54Ca2z+T+AtJHz2qEcDsgAkQfwAQ5ieGV1QjgD4BHRC/T1Thb0ejTmIYVusBm2W/ERoQvw8g/ERDqcD63t7eIym+RlcUJWCMsQLCTzxttKS4tbW1S/YbAc/vAQg/dUgdBcDze2MbhJ8qtChgnYwXD/G7pLW1dbP6ZQHpolZdNSgdEL8LVM/wWOwHCvwwrO6nIB0QvwPqDjtoF00v0jYBQfzObEHLbmp5TuZ1AKj226CG+6/FdoAgCK/09vaul/kOQvwWqNN6fViWm0p29/b2rpHwumeBsN+aLgg/lexWd0eWHnh+E1SvfwS5fuqgyv4atPZeAZ7fHGy9lT5I+Gsh/KtA/OZI3/edMii8/RLW9c8G4jfQ2tq6Hl4/dWQYY7fIfhOMQPyFwOunE3yuBiB+HWo3HxbupJOl6vFoQAXinw28Q7qRuqnHCMQ/GykXeEgEPl8dEL+KGvKjqSfdIPTXAfFfBV5BDvA5q0D8Vwml5bOqvJjdfWNjGG8FzIH4VSD+q4Sy9/437rqG/ejLq9nnr2sI4+1AIavV9m3pgfivHrUlnIV1Fazzjitlhe6HPsNWNtZEedkyI33ezyD+GUL5MnQ/dMPM/68uL2YvfOtzMADRIP2qPgbxzyD8y7B6eT1bs7x+1u9gACJDevEziH8G4Z7/G3etNP09DEAkSB/2M4h/BqHz+2ZeX49mAFAEDI1ata9DaqQXfxjFvvtuanJ8DhmAf/3qTexeF88FXID4YzCGqBH6JaAK/703N7t+/oaHPsMe/btrI74lUiB93g/xCxb/39/hPaOg6cBNX71JaQiSFbr2B28Xmo1JP9cP8Qv2AF68vp47r2tgL3xTzkIg1Uh++d072D/dfY1IAyh90Q/iFwgV8KoDfHmvWVSjFAJlqQOQ0CnleeGbt7LGuRXKvWsVVwRFzh+DMUSNsMM37/Pp9fWQAKgOkPY0gAzlqz9om+mA1Oi8Q5hGpV/BCfELgoR6J0evdacqjrRNB1Jas/mbtyozHWZREkU/VDQVgew9/lKLX+TabhHhqjYdSGIRJYiwoPFvePAG9stH77DtgWCC7qWK1Hm/7J5fmOW/8/qFol5aEcurP/hbJT9OWipA411/10pl/G6LoX6LpsAeeeeSBMI75LeC8mMSxta3jrBfv3WUjU1MxetG6CBPTy3OfoSshf6nh8Z5D4s8/3beL5oUZBe/kGk+pzCWJ9WKJ71GKYzt+PA0e/nNgyJE4huqUVDhM6gxpNB/69tHeQ9P6pxfdvELQWCOagkZAfKq9Og7PMh+v/ME6/3rmUiiASriaYKnKTserFlRL0L8UgPxCyBMz2/GGnUh0aMTU4ohoIhAtCGgxhwyejwFr0dQGiX1XL/s4ude7aXcVMSX3w/Vau2BHhsYYwdOXVCMwYFTI+zU0ATbfXjQ1+uSZ6drXLWoZsbQhAEZGL9jtgDilxjuOd9Ny+fG9m5S4YweekYnphRjQPSfH2f9hnpBo86YNUZs2Ojecha/1Mgufu5EHfJ7haKDmTEvj/dYr4zzYAxGkg5kn+fnTtLEnyQE3Ft0+AE+0Px+XPL9tMJ5laPUh7LKLn6ui3quWTSH58sBE25CZMUN2cXPlTgX+9KCsWAJ/APxcwSeXzxIq/gB8XOkMeEr7ZIACqr8gPg5gpA0HHDGAR8gfk7gCxkeCP35APFzorqiJBXXkQRWcYywZD68A+LnBCr94cG5tgLxg2BUl8PzhwXCfj5A/JxAsS88MKXKB2nFz3vzTnj+8AhyFgK4isyen+uiDnj+cEn67sVxAGE/SCTI+4MD8XNgNbrOQAKB+EEiwdRqcCB+ACQF4ucAvBBIIlLPmdAe999Tj7z681/PsJffPKDscAviT5D9/OiQk/tublJmDE6dH9+Q69nUl23bOCTbxy6t+L9//3VrHrytZeZnOlmGvlDPvbqb/d/+gUjHBsRQWlzEfv7IbbMWYS2aW/EFxthmsgmy3XZpw/67b2ws+LApAvi3h2/x/Fpo8EkGXV+6zmr15cO5nk3S9fhLKX76oGsrSy03b/TaQIIGn2Rw84p5duPsku1+yOr5bQ/oRANJ/PGzo4/D5yrk0NY4I6v4bUM8VO/Th4tGLOm28YbnNwF78aUPLAYqBPP8JiDsTx9udv/J9WySKvSH+E3ADrHpA3sAFALxW4Alo+nCZSrXJ9M9kVX8jh8yQv904WY6VrYuP1nF7/gho+KfHlxuqz4s232B57cAXXvpwWUUJ1XIzyQW/xGnJ6BrLz243Ocf4peBbNtGxw8a1eH04PKzhPglosfuUqkppAqNIakAlX5zZBb/dqcnwPunAxcp3LCbaDBtyCx+xw97FQ7fTDwuK/2OjiCNwPPbgLn+5OPyM4T4ZUJt6Nhtd8mo+CcfVPqtkb2919biI+ePL/3nx12Nzc1nmG3bCM8vIbYfutuKPzb9DJ/+IXfid1Hpt531STMQvwNuPMfoxGXxIwW+cJG6Sen1mezid5P3o+KfXFDpt0d2z8+cij2o+CcXN5+drPk+g/gVbMXvpuLvtvgE+OGmzuKi0m8b9aUdiN9R/M45/6mhCZ7jAS5wU2dxUeyTcopPQ3rxO4V96PFPLi7CfsfVnWlGevGr2G7k4OT9R8dR7Q8bN6mWi70Ypc33GcQ/g23451TxP9iPef6wcUq1sAejMxC/C1DxTx6o9DsD8V/Boc0XFf+4ceDUiO2IsAejMxC/C9xsBuG23RTwYWxiyvZ1cOqSMxC/CxD2x4tRB+EzfGaugPhd4nTQY9/hwRiNNt04hfwMpy65AuK/Qp3TExbVlYc0FBAUVPrdAfFfYY3TExBGxgen1l63n1WuZ5PtUe1pB+J3CcLI+OAkbg+VfogfOONUPcauP+Hh9Fmg0u8OiN8ldt6Gcsw7r2uI2YjTC/Vd2BVgkaK5A+K/gqsFHlZfuO998VruAwL2fOOulZZ/d5uiocMPEFvc3AWziv+GB2+A148AEjjdeyMeKv2vpOZm+ATiv+oB7L4MtOrvuTmVpae1X3z+ugb279+9g917c3M4gwQF0L3/jyf+lt17U9PMn9SQv8NhY07axKNL9juayefzMRhGPMj1bFrLGFunm/ojo9CXbdu4jX44/ZuNPx8au/wI9vOPJzQFWFZS9NdlDz1/Pbs6lad9nvT/h9TPdIu6f6PUQPweyPVsoi/Sa4kZsJy8kG3bKL1XdwPCfg9oEQCINVIX8bwA8XtH2kMeEgLE7xKI3zvw/vFlN3J590D83oH44ws+Gw9A/B7Jtm2khqCjiRq0PED8HoD4/YEvWfw4mm3bKPU+/F6B+P3hqiMQhAoKfR6B+H2gehiE/vEC0ZhHIH7/4MsWH4bRg+EdiN8/CP3jAz4LH0D8PkHoHysgfh9A/MHYnOTBpwRU+X0C8QcDeWb0wAD7BOIPgNrwg17/aEHI7xOIPzj48kXHK+jl9w/EH5Bs28YtTuf7A2HA8AYA4ucDvoThs1v2DTiDAvHzAUWn8ME9Dwi28eJErmcTeaG2VFxM/KGOPsfzFYE98Pz8gCcKD9xrDkD8nFB7y9HxJ55hiJ8PED9fnk3TxcSUbZje4wPEzxF12g/eXxzDOGyDHxA/f+D9xbEZXp8fED9nVO+/O1UXFQ+OItfnC8QvBoSm/OmC1+cLxC8AFwd/Am/0YKce/kD84uhCzz8X6B6uT8F1xA6IXxBqiIovbXC61KXTgDMQv0DUUBXhv39eVwuoQAAQv3i6UP33xVFETmKB+AWjC/+R/7uH7tU6VPfFAvGHgLrBJKb/3NOFTTnFA/GHhJq7PifFxQbjOeT54YD1/CGT69lEX+yHpbpo99CefMjzQwLij4BczyYKaVdLd+H2UCPP2jgPMG0g7I+GtZgBmAXdi3UxGo8UQPwRoFaxYQCuQPdgLSr74QPxRwQMgAKEHyEQf4RIbgAg/IiB+CNGZwBkOvarB8KPHlT7Y4Qk04CYzosJ8PwxQhVFmhuBnoPw4wM8fwzJ9Wxar25ZVZuSSxpWW3bRuRcjIP6YkuvZtCafn347kykqT/J1TOemWFG2+Cb06scPiD/GDP/m8R2lZRWfLy2rTOT4Jy9dZJOT46dq7/9JUwyGAwwg548xeTa9c2T4DBsdGWD5/HRyxp2fZmOjgyyXm2LVNfM+jsGQgAkQf4wpymQP0egmJkbZ0OBJdvnyROzHTGOksWaLS1lF5ZwYjAhYUYw7kwzIiw6f71cEVVlVxzKZeNlt8vYXx4bYxPgoq53byIqLS2MwKmAHxJ8wxi+OKAKrrqlnZeXVsRg85fYU5lP1CMJPDhB/AiEve2FkQEkHKAooKYlmQoBCfPL2lycnFMHXzW2MXUQCrIH4EwyJbniyn5WUlodqBPSiJ2g2ombOfAg/YUD8KUBvBMrLq4WlA0bRE/ReJHyQPCD+FEGipAcJlLxxRcUcarAJdIHUpHPp0kU2MT6iFB31ULRBD5BMIP4Yk5ueutfP6EikVBikB+XiFBGQMaD/7yY0Jw9PRoQKeVNTk6bPqZ4zX4kyQHKB+GNMhmUCJ/EkXnqQISCy2WIlGsgWFc9EBVRApOfkp6ctxa5BxoPCfA9dhwgNYgrELxkUFdDjso/LJuH7mMrDRqUxBeIHriDBU6iPOfz0APHHmFzuciy8Jgm+FnP4qQOfJrCFcvugws/1bMJ+/DEEnj/GTE/nIi2nYw4/3cDzx5jp6Vxkxrmqup6n8NfweiHAD3j+mHL+je/fGMXIKLyvqqnnPYeP6b4YAvHHlGxR8V1hj8znVJ4b4PljCMQfU6anpz4X5shI8DW1DUoTkADg+WMIxB9T8vn8irBGFsKqvDZRLwz8g4JfTJmuWnh9GCOjnYHm1DYIncPPZ8vY6L4/3CnsDYAvsHtvzDg3OEj5Me1vv3r6w/9k06feFTbAMBbn5Mrns4tL7lL+S4d2zKuvf1boGwLXwPPHhO7u7mWHDh3qZoxt1/rhi67/Miv6VDv3AZKXr6tvCkX4oyvWacInnjk3OHhk7wcf3E/XK/TNgSPw/BHS3d29jg6szOfz/5jJZBr+4StfYUuWLCkYUP7Ueyz34atcBhpWq64m/Hx29szBwMA5tm/fR+ztt95imUzmeD6f/2/G2Lbnn39+u9ABgQIg/hBRvd3aqampb2Wz2c9mMpky/btbiZ9pBuDjNxibGvc9YMrvqXlHNFNVzWxs6X0Fwid27upjv/rVy2xZy2Ljny4wxt4kQ6AaA5zgKxhU+wXT3d295tLk5CPZouyXiouzysk1xcXeb3tm0S0sW9PEcu/9wrMBENS4Y8rk3GvZxcXWLQqVlRVWf6phjD2gPl7esGHD3kwm81+qIcBRXwKA+DnT3d1dNz4+8eVMUebrpSWlnysqypSUlfJpmslUL2LZWx7xZADCXIrrJHwvZDKZGxhj9HjmqaeeOltUVPRbNSrYjqiADxA/ByicHx0b+1FJSckXykpLmysqxO2iSwag+PPdLLfzFyx/4aTtc2lhDu3vH8ZSXJ7CN1JUVLSAMfaw+mBPPvnk9mw2+7oaFRwR8qYSAPH7pKvr8X+ezue/WlZW+tnibLayuqoqvDcvLmfZmx+xNAA+ttoKxMXFd7PJuZ8O5b2YshVZlpYI0+MnTz751JFstuh1FA29A/F7oL2jc21d7ZxHFy1c0FFRUR7tNKlmAD58leXPfjDza9q7n8J8QW26BYQtfCPZbBEVUR+jx4YNGyZGLow+9NLPXvxtZANKEBC/De0dndSTvk59kKepvTA6ypoXLYzHAMkA3Pgwo2agfP9OZRvtMA/HjFr4Rqbz+fJPTvb/T3tH5261X2LLG69tRbHQAojfQHtH5zKd4At60nO5aXZhdIzVVIcY5jtAzUDl1XNZ+fD+UN5Paddd8YC+eScWXLgwqg1jtfp4rL2jc1ibPnzjta3bYjXgiIH4rwh+jU7wjvvmjVwYjZX4iYmWe9j0+RZW+ckfhL4PD+GXl5e5eJZ3RkbHzP5NrVYsbO/opJ9f1xkDqWcNpBW/Kvj1quCXevm3FPozxj/0vzQR7Px9LQQXZQB4efxsUZbbmDRy09N6z2/HTC9Be0en1IZAKvEHEbweUaH/mTNn2DWrVgV6DTIA1FlX+cmbLJO7xG1sJPixZfex6ZIabq/JE5fCNyK1IUi9+HkJ3sj54ZHYhf4al+csVzx09aHXuRgAqz79OGER8ntBOkOQSvHrinZdPAWvhzwNhZrZongujLwi2OAGgLfwL14c557zX7485dfzW2E0BFvSWCxMjfh103JdYR0RRV+4ulp+U2sU9vOEhDvy6a+x6kPbWHZiwPMrU9fe+KI7uXr8Y8eOU8ce1+scGeUqfCOKIdDNGpAhSEUzUeLF397RqVXpHw77vSnU5Cn+S5f45egaJFzy3F4NgMh2Xd4MDY+E8Tb6WYOj6oYrZAgS216cSPGrYX0X7zzeK7xD/1wuJ2ScmgGgWYCSkcOOzxctfJ5hP4X8ExP8jaYD9J17hh7tHZ09qhHYEvYggpIo8bd3dK5Xi3ex2RCSZ+h/8qT9Qp0gkAGgNfY0C1B6/iPLV5pYeCubaLhV2DgGBga41kkEh/xuoO9iW3tH52Y1GticlGgg9uLXefn1augVK3iH/qLRPLqZAQijXXfg3DlWnOH3eiGF/G6o1dYYJCUaiK34aRGNKvoHYjAcS8jzU+hZUsLnVg4PD7PaWrE2jgzA5comVnXizZnfjTXfxS6H0KfPUfdRhfxu0EcDm+NaG4jdPBWF9u0dnXSj/hh34WvwDD1HRsLxZJfrr2WHatcqXXtH531B+TkMhjl66nPnz4cy5gDUqrWBw+0dnVvUnpPYEAvPr07TdamP2IX2TlDoOW8un0NpyPNb7ePHm/qW69l7B0rZNU0rQ3k/Ijd1mdtrjVwI3NgTJtpMAaUEz8ZhujBSz0+ib+/opH3cj6gWMnHCJyj0pBCUByPDw6GN+/Tp0+zpH/6QjY2FI6KLFy9ye62JS3TP+RmSEKGU4I/tHZ3b1QJ2ZEQi/rSIXg+v0H8wxFCWhK8ZgDA4dvy43QaenohRoc8vbWoH4ZGojECo4k+j6DV4fRkHzp5l09PTXF7LjhdffJEdOHBAeUZfXx/bskV8YXpoaJh23uHyWgkL+e1YqhqB7WqROzRCE397R2dXGkWvwSv0HxoaYv39/byGZcruvj629de/nvWnLS+/zA6qxkAEExMTbM+evay8LHiDT4JDfjv06UAohUHh4idrplbvf5JG0evhUX2emppSDMCFCxd4Dm0Gyu+ffvpp07/R70Xl/9TANDHh/8ARPSkI+e0gI7BLnR0QerS5MPGrIf42dcoushbcMOEVip47d04Ri4jwn/L7UYv6BEUczz//PPf3PHv2rOL5zw/yqWekXPwaNDsgtB4gRPzqYpsjSZmn5wWFohMcFueMj48rff7Hjx/nOj7K6ym/11NqOFBkR28v27p1K7f3pAo/iZ+YnJwM/Hq0iQptpiIJtbp6APeDTbmKX+ftX0t7iG8FD680rk6JUQg+ODjIZVyUz1Ner6ekpIT2wFf+q4dX/k+Ry4kTJ2Z+zk0HX7g0wnfdflKgVKBPdarc4CZ+tUixXTZvb4RH6K/v8tNC5iCY5fm0pl47M5D+q19jT2kBhf9B83/an0ArzJ05c5ZLse9C9At5ooKc6WtqyzAXuIhftUjbw9pEI87wCP314qfwP+hqPxKycQbBGO4bf6ZpwJ/+9Ke+35PCfX3UMjwyEngpr2QhvxWPqWlA4GJgYPGrBQlpw3wzgob+lPPrIc/vd/qP8nfK4/WQ0DOZ2Uts6Gdj+P/73/2O/XnHDs/vaQz3mVJMPB14Ka+kIb8ZlAYENgCBPg1V+C+7eKpU8Aj9qeKvh7yo1/ZYyttfNHhvyvHpYYYx/Gdq1EBdgF7Qh/saPGoXYxf5TBWmhNVBDYBv8as5Prf8Iw1Q91rD/Hls5fKWwFeTz+cLfkfe1O30H+XrZtN2Ru9uxBj+U/7vpf3XGO5rTHFY0EP3le4vry7BFLBa3VfQF77uompttiHUvwIJis7vW7VyOVswv57LTjXkOY1emH5nDKetoHz9gKFibxbuGzEL/+l1qB3YCbNwn6n3Z4pDRx7dV7q/dJ9hBGZoU1vmPeP37gnbEjtJaKL/1Mplym4+PLenoh7/lpbCCII6/6gD0A76u3EsduG+EbPw/y9vv+3YdWgW7hPjExNcKv0aeiNA998pmpGALj/hfxDxS4tR9CI4rqyAq2T19fUFr045uFVvO/2e/v7F++9nX/v612d+71Ug+vB/8eLF7PHHH7ftOrQK9xcsWMBOnDjJbacjPWQE6P7T5yC5Eaj1o0nP4leLfFKG+1WVFcJFr4c8aWNjY4HXtJv+I6Oh7QJ8++23s29/+9usrq7OMdw3ooX/f3PbbeyHTz/NKiorLbsOySCY/Z6MF4n/6JGjnq/dKzACzHMbsB/Pz7XLKAmQ6Je1LFYeYW7WqQmqqbm54G9U0NPaZjVoOtDYEHRnayv78Y9/zBoaGjy//7333ce+853vOL4v5fnGbccpbWhWx31ucPbMhUj0RqCK094BCWGp1yXBfsQvTQefXvRRfJG0E3zKy8tZ48LCU4H13X+UjxvDbhJgU1MTW7FyJXvppZfYihUrXL83RQxPPPGEImBj/k/vq0070vua1QIoYtE88KSAw0icICMQ5WcXEZ4csyfx8+4tjjNx+OLoj++qnzdPCaONUHRAeb5ZGkDC1Yp8VdXV7GcvvaSkAnZUVVWxf3niCbauo0N5Fgm4YcGCgn9B3t7qfWtqapRUg/ho3z5uu/f4QW/AJUCc+Bljoe40EiUXY9BQcvbMmVlHeNHGnmbTf/v37y8Iu0mA9PAKRQf33HPPrH9lZnis3leLNjT27PkgFoeZSrIMeKmX1X8QvwVnBs4pveRRc/zYsZkRkBdvNsn/jRgFqMepW2/Pnj2mvzczPGbQ9KR+SvHEiU8iv4ckfEnEz7xo1LX41XlEqRbunDjVz2V9fhCMJ/cqIbXDoR76cN/IoUOHHEdjZiDcGB6aljRGCLw28PALfX4nTnlrT044/MXPGIvVgQNhQCvI6IuTC2FDTSvMptAW6oppRkiAVuH++++/7+o9Dx08aPp7u1SCpiOpyKeHjuMuKXHXWCQC+tyOfXIqsvePCNc69SJ+aUJ+PbQxZ5Sew0z8Vl6YDMICk+KchpWojRy0iQ4onTAL/82mIz/48EOunX1eOf7JyTRu9OnEarfdfvD8LqDz+M6dt2+pFYldA40eu3CfOYhaz/u7d1v+zczw0DQkTUca2bfv40juF9F/5qzMqwBdaRXid0n/6ei+TPqinx4Sv+ZZzfJtI249v1NRUB/+Ky3I8+aZPu/cuQFX78cbKu6dG4zOWMcAV1G6K/GrYYT0C3mOnzjJ7VguL+y32U9vSUuLYgDswn0NN8U+phYZxxy2y6Lwn9IMq3MFKd+vNIkGREMFPvL6ksPV80vt9TWoAHjsxMnQC4DG+X49JEDq4HNasee22KfhlCLQ+61atcryfd/fs0fIYh47tAIftvpirub63YpfymKfGVQApBQgbKxCf7e4Dfk1vD7fiHEvgTCQtMBnhqspebfi575neJJRcsqQC4DHAu7h77bY5/f5Rs5z2nLcLZIX+Apws8gHYb9Pwi4AHti/P9C/D9Pz79zVF2o/P3ViSl7gM8PRYbsVv/RbcptxPMT8n7bzHg5wdr/bYp/f5+uhFuGw+vmvdPCJPdg0oQQXf1gnhiYRKiwdORZe77rfPNprsU/Dr/c/fPiwr3/nFTK8SgcmCnxmcAn7ke/bEGYHoN+in9ettzX85P00xVcc0saadN/p/gNTuIT98PwOhLVqjDy/1ZSfHX49uB+jQVN8YbT0nh0YVDovgSWOfTnw/JxQvFAIKwD9eP+DPsVv1+Zrxa5du3y9lxeowEdLroE9ThV/iJ8jlP+LLgDadftZYbVG3wmvRb+BgXNc9ue3AwU+T9gu8HEj/rbwx5xMqPBEjSYi8TrlF2TKjjbr9BL6v/Puu6ymusr3+zmBAp9nbFN2W/HzOAlUNmjuX2RvOeX8XgxA0GYdL+J/9913A72XEyjweca/+FHs8wc1nIgsAHrp9gvaput2mpBC/kuGbcN5ggKfLwKF/cj3fULeX1QB0JPnDyh+t8Zj565drKamOtB7WUHRFAp8vrBN2SF+QSgrAGmFmYACIHX7Gff2s8JvsU/DrfH4y1/+V0hXHy2hpk5K4A+71B1hv0BohZmoAuDevXsdnxM05GcmG4iaQSH/5CX+Ib+yRJdaqFHgC4Klhp3Ej4JfQEQVAPd/7LxFVtBin4ZT3i8q5KfFUyjwBcYyencSP6b5OEAFQAxKhb0AAAWwSURBVN5nANARWU5emYfnd/M6IkJ+WjIt0V77IvEtfsAJEWcAOIX+QYt9GnbTfSJCfiVaimDDlJTiXfxeT/wE9og4A8Ap9A+yLFePnRHhHfKjwMcdX54f+T5neK8AtAv9yVtThx4P7GYMeIb8KPAJwZf4UekXADWqUMMKL6xCf175vt3r8Q75UeATguXqPnj+COB5CKhV6M+r0q9hlvfv+PNb3EJ+yQ7TDBWrDXng+SOCCoA8zgCwCv39LMe1w8yYvPfee1xCfgkP0wwbU0cOzx8RPM8AMAv9eRX7NIzGhHbsKcoEf126/jC3QpMUz54fm3YKhtcZAHsNBTmexT79a+r5U+8OLst3lT0QUOATjWfPD0KAxxkAk5OTsxb78C72MZMjvPbt2xf4NbFENzRMK/6m4sccf7jwOANAv8MP72Kf8XU/2rePlZUGO4oLBb5QcS9+ED5BDwH9WOeJeRf7Zl5X7fH/0596A23SiQJf6HgSPzx/yAQtANIKQq3wx7vYp6Hl/UePHvX9GijwRYLpXD88f4wIWgCkvJ/yct7FPg2qJdBRXNWVlb5fAwW+aGjv6Czw/lbixxx/RATJhWlf/30ulvr6hSKK3t4dvo/eVnY3QoEvKlyLH3P8ERLkDIB33nlH2MCLiorY2TP+cnVlVgOHaUZJgabh+WOK3zMAhofECaysvMJXOy8ZMpE7GgNXFGjaSvy1uJ/R4vcQUPLO5eXlQsa+ZMkSz+28yko92ssQeX7sKPgkzQoDIBr8LgFuaWnhPt5stpjV1XnPBmkPw8uCT/EBriiYwTMz4xB/jPBTAFywYAH3C6irr/fczkuhftDmJSAOM/Gj2BczvJ4BQGE/bwOwaNEiT89HgS92uMr5UeyLGX7OAOAp/tKyclbvIeRHgS+WFNTx0OSTELyeAUCeurg4WP+9RsPCha7n9lHgiy/Geh48f4LwegYAD+9PswcLPbwOFShR4IstjuJHzh9jvBwCyqPqX1Vdw+rq3M384jDNZIFqfwJxWwCsrq5mNTU1gS6wubnZ1fNoT0Icphl7Zk33mYnfcrdPEA+8FAC9Vun10Nz+vHn1js+7skS3H9+OhIGCX0KhvNpNA1AQ8c9bsMBx3T4ZIOUwEhT4ksCset4s8WMHn2Th5gwAqvj7NQCLXYT82IorUcyq58HzJxw3ZwD4EX9FRSWbM8e+XoACX+KwFT+KfQnE6RDQuXPnel7s09TcbLuIBwW+RDJrR26IPwW4OQTU67Rf48KFln+jvQZR4Es+CPtTgtMKQC+hf2PjIlZebl7ow2GayUZ/dJdR/Cj4JRjKv63OAPBS+Gu0eR4O00w8M3k/PH/KsDsDoKmpyfFiy8rK2bz6uaZ/I8OCvfYTj6X4kfOnAKszAGgzDqfCH+3WY4ayroDD0WIgcizDfnT3pQC7MwCcCn+LmgpDfjIkZFBAukDYn1KszgCwC/2p0FdaUjLrdyjwpY5Cz4/uvvRhdghoNpu1LPw1Ly7s6EOBL3Wg4CcLingNDUDNzYsLrp5qAXW1s5fuosCXbvTiR7EvpRjPAKitnVOw1Hf58hWzfkaBL7W0aRcG8UuA2RkAS5dere1SD8DChQ0zP5OhQIEv/SDslwRjB+D8+fNn9vhraGhQagEaOEwz3bR3dCp5v1782Lsv5ejPACCxa7n/ihVXQ34s0ZUCRet68WPvPgnQHwLa0rJEWfFXpm7YEeSEYJA8EPZLiFYALC0tZTfeeGWV55WtuPydwAsSR4Hnb8NnKAeUz2tnABQXZxVD4OdQUJBYCnJ+IBH6MwBQ4JMTpdyrVf+AXNAZAFTcQ4FPOpRuXs3zo9IvKThFV14Q9gMgH7NyfnT3ASAPyhQPxA+ApCDsB0BCqMgPzw+AnKyB+AGQFIT9AMjJMszzAyAnM+KvxRcAALlA2A+AnCzL7ty9l0L+R/AFAEAiGBv+f8dx84PLOfNSAAAAAElFTkSuQmCC";
        Hashtable danhSachNhanSu;
        double defaultSize = 15.5;
        SQL Sql = new SQL();
        public MainWindow()
        {
            InitializeComponent();
            Icon = ChuyenDoi.BitMapImage(base64_defaultAvatar);
            ThongTin_NewLoad();
            CaiDat_NewLoad();
            InsertDataToHashtable(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\D\ModuleKiThuatLapTrinh\BaiTapNhom\Database.mdf;Integrated Security=True", "[dbo].[ThongTin]", danhSachNhanSu); ;
            HienThi.ThongTinList(lstvThongTin, danhSachNhanSu);
            txbSoNhanSu.Text = "Hiện tại có  " + lstvThongTin.Items.Count.ToString() + "  nhân sự trong danh sách";
        }

        public void InsertDataToHashtable(string connection, string SELECT_FROM, Hashtable hashtable)
        {
            if (hashtable != null)
                hashtable.Clear();
            Sql = new SQL();
            Sql.DocDuLieu(connection, SELECT_FROM);
            cmd = Sql.Cmd;
            con = Sql.Con;
            SqlDataReader dr = cmd.ExecuteReader();
            danhSachNhanSu = new Hashtable();
            while (dr.Read())
            {
                NhanSu nhanSu = new NhanSu();
                nhanSu.HoTen = dr["HoVaTen"].ToString();
                nhanSu.CMND = dr["CMND"].ToString();
                nhanSu.MaNhanVien = dr["MaNhanVien"].ToString();
                nhanSu.GioiTinh = dr["GioiTinh"].ToString();
                nhanSu.NgaySinh = Convert.ToDateTime(dr["NgaySinh"].ToString()).ToString("dd/MM/yyyy");
                nhanSu.NgayVao = Convert.ToDateTime(dr["NgayVao"].ToString()).ToString("dd/MM/yyyy");
                nhanSu.QueQuan = dr["QueQuan"].ToString();
                nhanSu.SoDienThoai = dr["SoDienThoai"].ToString();
                nhanSu.ChucVu = dr["ChucVu"].ToString();
                nhanSu.BoPhan = dr["BoPhan"].ToString();
                nhanSu.Avatar = dr["Avatar"].ToString();
                danhSachNhanSu.Add(nhanSu.MaNhanVien, nhanSu);
            }
            con.Close();
        }

        public void ThongTin_NewLoad()
        {
            txtHoVaTen.Clear();
            txtCMND_CCCD.Clear();
            txtSoDienThoai.Clear();
            cboGioiTinh.SelectedIndex = -1;
            cboQueQuan.SelectedIndex = -1;
            dtpNgaySinh.SelectedDate = DateTime.Today;
            dtpNgayVao.SelectedDate = DateTime.Today;
            txtMaNhanVien.Text = "Mã được cấp tự động";
            txtBoPhan.Text = "Phân tại TỔ CHỨC";
            txtChucVu.Text = "Phân tại TỔ CHỨC";
            ChuyenDoi.HinhAnh(base64_defaultAvatar, imgAnhDaiDien);
        }

        public void FontLoading()
        {

            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                cboKieuChu.Items.Add(fontFamily.ToString());
            }
        }

        public void CaiDat_NewLoad()
        {
            sliderFontSize.Value = defaultSize;
            cboKieuChu.SelectedItem = "UTM Daxline";
        }


        private void imgAnhDaiDien_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileSource = new Microsoft.Win32.OpenFileDialog();
            if (fileSource.ShowDialog() == true)
            {
                imgAnhDaiDien.Source = new BitmapImage(new Uri(fileSource.FileName));
                urlAvatar = fileSource.FileName;
            }
        }

        TraCuu frmTraCuu;
        private void btnTraCuuNhanSu_Click(object sender, RoutedEventArgs e)
        {
            if (frmTraCuu == null)
                frmTraCuu = new TraCuu();

            // you need to take care of form closing because the object being disposed)    
            frmTraCuu.Closed += delegate { frmTraCuu = null; };

            if (frmTraCuu.WindowState == WindowState.Minimized)
                frmTraCuu.WindowState = WindowState.Normal;

            frmTraCuu.Show();
            frmTraCuu.Activate();
        }

        private void sliderFontSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FontSize = sliderFontSize.Value;
        }

        private void cboKieuChu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FontFamily = new System.Windows.Media.FontFamily(cboKieuChu.SelectedItem.ToString());
        }

        private void btnThemNhanSu_Click(object sender, RoutedEventArgs e)
        {
            txtMaNhanVien.Text = createMaNhanVien();
            NhanSu nhanSu = new NhanSu();
            nhanSu.HoTen = txtHoVaTen.Text;
            nhanSu.CMND = txtCMND_CCCD.Text;
            nhanSu.MaNhanVien = txtMaNhanVien.Text;
            nhanSu.GioiTinh = cboGioiTinh.SelectedValue.ToString();
            nhanSu.NgaySinh = dtpNgaySinh.SelectedDate.Value.ToString("dd/MM/yyyy");
            nhanSu.NgayVao = dtpNgayVao.SelectedDate.Value.ToString("dd/MM/yyyy");
            nhanSu.QueQuan = cboQueQuan.SelectedValue.ToString();
            nhanSu.SoDienThoai = txtSoDienThoai.Text;
            nhanSu.ChucVu = txtChucVu.Text;
            nhanSu.BoPhan = txtBoPhan.Text;
            try { nhanSu.Avatar = ChuyenDoi.Base64(urlAvatar); }
            catch { nhanSu.Avatar = base64_defaultAvatar; }
            lstvThongTin.Items.Add(nhanSu);
            txbSoNhanSu.Text = "Hiện tại có  " + lstvThongTin.Items.Count.ToString() + "  nhân sự trong danh sách";
            danhSachNhanSu.Add(nhanSu.MaNhanVien, nhanSu);
            query = "INSERT INTO [dbo].[ThongTin] (HoVaTen,CMND,MaNhanVien,GioiTinh,NgaySinh,QueQuan,SoDienThoai,BoPhan,ChucVu,NgayVao,Avatar) VALUES (@HoVaTen,@CMND,@MaNhanVien,@GioiTinh,@NgaySinh,@QueQuan,@SoDienThoai,@BoPhan,@ChucVu,@NgayVao,@Avatar)";
            con.Open();
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@HoVaTen", txtHoVaTen.Text);
            cmd.Parameters.AddWithValue("@CMND", txtCMND_CCCD.Text);
            cmd.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
            cmd.Parameters.AddWithValue("@GioiTinh", cboGioiTinh.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.SelectedDate.Value);
            cmd.Parameters.AddWithValue("@QueQuan", cboQueQuan.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@BoPhan", txtBoPhan.Text);
            cmd.Parameters.AddWithValue("@ChucVu", txtChucVu.Text);
            cmd.Parameters.AddWithValue("@SoDienThoai", txtSoDienThoai.Text);
            cmd.Parameters.AddWithValue("@NgayVao", dtpNgayVao.SelectedDate.Value);
            cmd.Parameters.AddWithValue("@Avatar", nhanSu.Avatar);
            cmd.ExecuteNonQuery();
            con.Close();
            ThongTin_NewLoad();
        }

        static string createMaNhanVien()
        {
            Random rd = new Random();
            string maNhanVien = "";
            for (int i = 1; i <= 12; i++)
            {
                if (rd.Next(2) == 0)
                    maNhanVien += Convert.ToChar(rd.Next(65, 91));
                else
                    maNhanVien += Convert.ToChar(rd.Next(48, 58));
            }
            return maNhanVien;
        }

        private void lstvThongTin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                NhanSu nhanSu = (NhanSu)lstvThongTin.SelectedValue;
                try { ChuyenDoi.HinhAnh(nhanSu.Avatar, imgAnhDaiDien); } catch { }
                txtHoVaTen.Text = nhanSu.HoTen;
                txtBoPhan.Text = nhanSu.BoPhan;
                txtChucVu.Text = nhanSu.ChucVu;
                txtCMND_CCCD.Text = nhanSu.CMND;
                txtMaNhanVien.Text = nhanSu.MaNhanVien;
                txtSoDienThoai.Text = nhanSu.SoDienThoai;
                dtpNgaySinh.SelectedDate = Convert.ToDateTime(nhanSu.NgaySinh);
                dtpNgayVao.SelectedDate = Convert.ToDateTime(nhanSu.NgayVao);
                cboGioiTinh.SelectedValue = nhanSu.GioiTinh;
                cboQueQuan.SelectedValue = nhanSu.QueQuan;
            }
            catch { }
        }

        private void btnXoaNhanSu_Click(object sender, RoutedEventArgs e)
        {
            if (lstvThongTin.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn đối tượng để thao tác", "LỖI", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            query = "DELETE FROM [dbo].[ThongTin] WHERE MaNhanVien=@MaNhanVien";
            try { con.Open(); } catch { }
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            danhSachNhanSu.Remove(txtMaNhanVien.Text);
            ThongTin_NewLoad();
            HienThi.ThongTinList(lstvThongTin, danhSachNhanSu);
            txbSoNhanSu.Text = "Hiện tại có  " + lstvThongTin.Items.Count.ToString() + "  nhân sự trong danh sách";
        }

        private void lstvThongTin_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            lstvThongTin.SelectedIndex = -1;
            ThongTin_NewLoad();
        }


        private void btnSuaThongTin_Click(object sender, RoutedEventArgs e)
        {
            if (lstvThongTin.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn đối tượng để thao tác", "LỖI", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            NhanSu nhanSu = new NhanSu();
            nhanSu.HoTen = txtHoVaTen.Text;
            nhanSu.CMND = txtCMND_CCCD.Text;
            nhanSu.MaNhanVien = txtMaNhanVien.Text;
            nhanSu.GioiTinh = cboGioiTinh.SelectedValue.ToString();
            nhanSu.NgaySinh = dtpNgaySinh.SelectedDate.Value.ToString("dd/MM/yyyy");
            nhanSu.NgayVao = dtpNgayVao.SelectedDate.Value.ToString("dd/MM/yyyy");
            nhanSu.QueQuan = cboQueQuan.SelectedValue.ToString();
            nhanSu.SoDienThoai = txtSoDienThoai.Text;
            nhanSu.ChucVu = txtChucVu.Text;
            nhanSu.BoPhan = txtBoPhan.Text;
            try
            {
                nhanSu.Avatar = ChuyenDoi.Base64(urlAvatar);
            }
            catch
            {
                NhanSu tmp = (NhanSu)danhSachNhanSu[txtMaNhanVien.Text];
                nhanSu.Avatar = tmp.Avatar;
            }
            query = "UPDATE [dbo].[ThongTin] SET HoVaTen=@HoVaTen,CMND=@CMND,GioiTinh=@GioiTinh,NgaySinh=@NgaySinh,NgayVao=@NgayVao,QueQuan=@QueQuan,SoDienThoai=@SoDienThoai,Avatar=@Avatar WHERE MaNhanVien=@MaNhanVien";
            try { con.Open(); } catch { }
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@HoVaTen", txtHoVaTen.Text);
            cmd.Parameters.AddWithValue("@CMND", txtCMND_CCCD.Text);
            cmd.Parameters.AddWithValue("@GioiTinh", cboGioiTinh.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.SelectedDate.Value);
            cmd.Parameters.AddWithValue("@QueQuan", cboQueQuan.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@SoDienThoai", txtSoDienThoai.Text);
            cmd.Parameters.AddWithValue("@NgayVao", dtpNgayVao.SelectedDate.Value);
            cmd.Parameters.AddWithValue("@Avatar", nhanSu.Avatar);
            cmd.Parameters.AddWithValue("@MaNhanVien", nhanSu.MaNhanVien);
            cmd.ExecuteNonQuery();
            con.Close();
            danhSachNhanSu[txtMaNhanVien.Text] = nhanSu;
            HienThi.ThongTinList(lstvThongTin, danhSachNhanSu);
            txbSoNhanSu.Text = "Hiện tại có  " + lstvThongTin.Items.Count.ToString() + "  nhân sự trong danh sách";
            lstvThongTin.SelectedItem = nhanSu;
        }
    }
}
