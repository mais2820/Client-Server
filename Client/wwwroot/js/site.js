// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//function btnwarna() {
//    document.getElementById("col1").style.backgroundColor = "Green";
//}

//function btntulisan() {
//    document.getElementById("paragraf2").innerHTML = "Tulisan sudah diganti";
//}

//function btnwarnatulisan() {
//    document.getElementById("paragraf3").style.color = "Red";
//}

//$.ajax({
//    url: "https://pokeapi.co/api/v2/pokemon/"
//}).done((result) => {
//    let temp = "";
//    $.each(result.results, (key, val) => {
//        temp += `
//                <tr>
//                    <td>${key + 1}</td>
//                    <td>${val.name}</td>
//                    <td><button onclick="detailSW('${val.url}')" data-bs-toggle="modal" data-bs-target="#modalSW" class="btn btn-primary">Detail</button></td>
//                </tr>
//            `;
//    })
//    $("#tbodySW").html(temp);
//});

$(document).ready(function () {
    let table = new DataTable('#myTable', {
        dom: 'Blfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print', 'colvis'
        ],
        ajax: {
            url: "https://localhost:7127/api/employees",
            dataSrc: "data",
            dataType: "JSON"
        }, 
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            { data: "nik" },
            {
                data: 'fullname', 
                render: function (data, type, row) {
                    return row.firstName + ' ' + row.lastName;
                }
            },
            {
                data: "birthDate",
                render: function (data) {
                    return moment(data).format('DD-MM-YYYY');
                }
            },
            {
                data: 'gender',
                render: function (data, type, row) {
                    return data == 0 ? "Female" : "Male";
                }
            },
            {
                data: "hiringDate",
                render: function (data) {
                    return moment(data).format('DD-MM-YYYY');
                }
            },
            { data: "email" },
            { data: "phoneNumber" },
            {
                data: '',
                render: function (data, type, row) {
                    return `<button onclick="Update('${row.guid}')" data-bs-toggle="modal" data-bs-target="#updateModal" class="btn btn-warning"><i class="fas fa-edit"></i> </button>
                    <button onclick="Delete('${row.guid}')" data-bs-toggle="modal" data-bs-target="" class="btn btn-danger"><i class="fas fa-trash"></i> </button>`;
                }
            }
        ],
    });
});

function Insert() {
    var obj = new Object();
    obj.FirstName = $("#firstName").val();
    obj.LastName = $("#lastName").val();
    obj.BirthDate = $("#birthDate").val();
    obj.Gender = parseInt($("#gender").val());
    obj.HiringDate = $("#hiringDate").val();
    obj.Email = $("#email").val();
    obj.PhoneNumber = $("#phoneNumber").val();

    $.ajax({
        url: "https://localhost:7127/api/employees",
        type: "POST",
        contentType: "application/json", 
        data: JSON.stringify(obj), 
    }).done((result) => {
        alert("Data berhasil disimpan!");
    }).fail((error) => {
        alert("Gagal menyimpan data!");
    });
}

function Delete(guid) {
    if (confirm("Apakah Anda yakin ingin menghapus data ini?")) {
        $.ajax({
            url: "https://localhost:7127/api/employees/?guid=" + guid,
            type: 'DELETE',
            success: function (response) {
                console.log('Data berhasil dihapus:', response)
                location.reload()
            },
            error: function (xhr, status, error) {

                console.log('Terjadi kesalahan:', error);
            }
        });
    }
}

//function Update(guid) {
//    var obj = new Object();
//    obj.guid = guid
//    obj.FirstName = $("#firstName").val();
//    obj.LastName = $("#lastName").val();
//    obj.BirthDate = $("#birthDate").val();
//    obj.Gender = parseInt($("#gender").val());
//    obj.HiringDate = $("#hiringDate").val();
//    obj.Email = $("#email").val();
//    obj.PhoneNumber = $("#phoneNumber").val();

//    $.ajax({
//        url: "https://localhost:7127/api/employees/?guid=" + guid,
//        type: "PUT",
//        contentType: "application/json",
//        data: JSON.stringify(obj),
//    }).done((result) => {
//        alert("Data berhasil disimpan!");
//    }).fail((error) => {
//        alert("Gagal menyimpan data!");
//    });
//}
//function Update(guid) {
//    var obj = new Object();
//    obj.guid = guid;
//    obj.FirstName = $("#firstName").val();
//    obj.LastName = $("#lastName").val();
//    obj.BirthDate = $("#birthDate").val();
//    obj.Gender = parseInt($("#gender").val());
//    obj.HiringDate = $("#hiringDate").val();
//    obj.Email = $("#email").val();
//    obj.PhoneNumber = $("#phoneNumber").val();

//    $.ajax({
//        url: "https://localhost:7127/api/employees/?guid=" + guid,
//        type: "PUT",
//        contentType: "application/json",
//        data: JSON.stringify(obj),
//    }).done((result) => {
//        alert("Data berhasil diupdate!");
//        // Lakukan hal lain setelah sukses update, seperti meng-update tampilan atau lainnya
//    }).fail((error) => {
//        alert("Gagal mengupdate data!");
//    });
//}




$(document).ready(function () {
    // Fetch data from API
    $.ajax({
        url: "https://localhost:7127/api/employees",
    }).done(function (result) {
        let gender1 = 0;
        let gender2 = 0;

        result.data.forEach(function (dataDetail) {
            if (dataDetail.gender === 0) {
                gender1++;
            } else if (dataDetail.gender === 1) {
                gender2++;
            }
        });

        console.log("Total Female", gender1)
        console.log("Total Male", gender2)

        // Buat data baru untuk grafik pie berdasarkan data gender yang dihitung
        var genderPieData = {
            labels: ['Female', 'Male'],
            datasets: [
                {
                    data: [gender1, gender2],
                    backgroundColor: ['pink', 'blue'],
                }
            ]
        };

        // Buat grafik pie
        new Chart(('genderPieChart'), {
            type: 'pie',
            data: genderPieData
        });
    });
});

$(document).ready(function ageChart() {
    // Memuat data menggunakan Ajax
    $.ajax({
        url: "https://localhost:7127/api/employees/"
    }).done((result) => {
        // Process the fetched employee data here

        const currentDate = new Date(); // Current date
        const ageCounts = {};

        result.data.forEach(employee => {
            const birthDate = new Date(employee.birthDate);
            const age = currentDate.getFullYear() - birthDate.getFullYear();

            // Counting the occurrences of each age
            if (ageCounts[age]) {
                ageCounts[age]++;
            } else {
                ageCounts[age] = 1;
            }
        });

        var xValues = Object.keys(ageCounts); // Get unique ages
        var yValues = Object.values(ageCounts); // Get counts for each age
        var barColors = "#b91d47";

        new Chart("umur-chart", {
            type: "bar",
            data: {
                labels: xValues,
                datasets: [{
                    backgroundColor: barColors,
                    data: yValues
                }]
            },
            options: {
                title: {
                    display: true,
                    text: "Employee Age Distribution"
                },
                scales: {
                    x: {
                        title: {
                            display: true,
                            text: "Age"
                        }
                    },
                    y: {
                        title: {
                            display: true,
                            text: "Count"
                        }
                    }
                }
            }
        });
    }).fail((xhr, status, error) => {
        console.error("Error fetching employee data:", error);
    });
});




function detailSW(stringURL) {
    $.ajax({
        url: stringURL,
        success: (result) => {
            $('.modal-title').html(result.name);
            $('#pokemonImage').attr('src', result.sprites.other['official-artwork'].front_default);
            $('#pokemonId').text('ID: ' + result.id);
            displayTypes(result.types);
            displayStats(result.stats);
            displayAbilities(result.abilities);
        }
    });
}

function displayTypes(types) {
    const pokemonTypesElement = $('#pokemonTypes');
    pokemonTypesElement.empty();

    types.forEach((type) => {
        const badgeElement = $('<span></span>').addClass('badge badge-secondary mr-2').text(type.type.name);
        pokemonTypesElement.append(badgeElement);
    });
}


function displayStats(stats) {
    const statsContainer = $('#statsContainer');
    statsContainer.empty();

    const statLabels = ["HP", "Attack", "Defense", "Special Attack", "Special Defense", "Speed"];
    stats.forEach((stat, index) => {
        const labelElement = $('<div></div>').addClass('mt-1').text(`${statLabels[index]}:`);

        // Hitung persentase panjang progress bar berdasarkan nilai dari API (rentang 0-100)
        const progressPercentage = (stat.base_stat / 100) * 100;

        const progressBarElement = $('<div></div>').addClass('progress-bar').attr('role', 'progressbar').attr('style', `width: ${progressPercentage}%`).text(`${stat.base_stat}/100`);
        progressBarElement.attr('aria-valuenow', stat.base_stat);
        progressBarElement.attr('aria-valuemin', 0);
        progressBarElement.attr('aria-valuemax', 100);
        const progressElement = $('<div></div>').addClass('progress mt-2').append(progressBarElement);
        statsContainer.append(labelElement).append(progressElement);
    });
}

function displayAbilities(abilities) {
    const abilitiesContainer = $('#abilitiesContainer');
    abilitiesContainer.empty();

    const abilitiesList = $('<ul></ul>').addClass('list-unstyled');
    abilities.forEach((ability) => {
        const listItem = $('<li></li>').text(ability.ability.name).css('list-style-type', 'circle');
        abilitiesList.append(listItem);
    });

    const abilitiesTitle = $('<h6></h6>').text('Abilities:');
    abilitiesContainer.append(abilitiesTitle).append(abilitiesList);
}




















