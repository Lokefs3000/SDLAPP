var canvas = null
var ctx = null

var rightclickopen = false
var onrightclickmenu = false

var objectselect = null

var objects = []

var obj = '<button id="$ID$" style="width: 100%; height: 30px;" onclick="select($OBJECTINDEX$);">$NAME$</button><br>'

function select(i) {
    objectselect = objects[i];
    document.getElementById("prop_name").value = objectselect.name;
    document.getElementById("prop_x").value = objectselect.x;
    document.getElementById("prop_y").value = objectselect.y;
    document.getElementById("prop_w").value = objectselect.w;
    document.getElementById("prop_h").value = objectselect.h;

}

function createImage() {
    var n = {
        id: objects.length,
        x: 0,
        y: 0,
        w: 100,
        h: 100,
        tex: null,
        name: "Item#" + objects.length,
        type: "image",
        path: ""
    }

    var o = obj.replace("$NAME$", n.name).replace("$OBJECTINDEX$", objects.length).replace("$ID$", n.id);
    document.getElementById("content").innerHTML += o;

    objectselect = n;

    objects.push(n);
    return n;
}

function createTexture() {
    var n = {
        id: objects.length,
        x: 0,
        y: 0,
        w: 0,
        h: 0,
        name: "Item#" + objects.length,
        img: null,
        type: "texture"
    }

    img = document.createElement("IMG");

    img.addEventListener("load", function(e) {
        n.img = img;
    })

    document.getElementById("fileselect_hidden").click();
    document.getElementById("fileselect_hidden").addEventListener("change", function(e) {
        var file = document.getElementById("fileselect_hidden").files[0];
        const reader = new FileReader();
        reader.addEventListener('load', (event) => {
            img.src = event.target.result;
        });
        reader.readAsDataURL(file);
    })
    

    var o = obj.replace("$NAME$", n.name).replace("$OBJECTINDEX$", objects.length).replace("$ID$", n.id);
    document.getElementById("content").innerHTML += o;

    objectselect = n;

    objects.push(n);
    return n;
}

var sn = "\n";

function generateCode() {
    var code = "";

    for (let index = 0; index < objects.length; index++) {
        const object = objects[index];
        
        if (object.type == "texture") {
            code += "TEXTURE " + object.name + sn;
            code += "PATH " + object.path + sn;
            code += "END " + sn;
        }
    }

    for (let index = 0; index < objects.length; index++) {
        const object = objects[index];
        
        if (object.type == "image") {
            code += "IMAGE " + object.name + sn;
            code += "POSITION " + object.x + " " + object.y + " 0" + sn;
            if (object.tex != null)
                code += "TEXTURE " + object.tex.name + sn;
            code += "WIDTH " + object.w + sn;
            code += "HEIGHT " + object.h + sn;
            code += "END " + sn;
        }
    }

    return code.substring(0, code.length - 1);
}

function withinBounds(x1, y1, w1, h1, x2, y2) {
    return x1 <= x2 && x2 <= x1 + w1 &&
           y1 <= y2 && y2 <= y2 + h1;
}

var mousex, mousey = 0
var mousedown = false

function onLoad() {
    canvas = document.getElementById("designcanvas");
    ctx = canvas.getContext("2d");

    document.getElementById("fpstarget").onchange = function() {
        fpsTarget = document.getElementById("fpstarget").value
    }

    document.body.addEventListener("mousedown", function(e) {
        if (e.button == 2) {
            rightclickopen = true
            document.getElementById("rightclick_menu").style.visibility = "visible"
            return;
        }

        if (!onrightclickmenu) {
            rightclickopen = false
            document.getElementById("rightclick_menu").style.visibility = "hidden"
        }
    })

    document.getElementById("rightclick_menu").addEventListener("mouseenter", function(e) {
        onrightclickmenu = true
    })

    document.getElementById("rightclick_menu").addEventListener("mouseleave", function(e) {
        onrightclickmenu = false
    })

    document.body.addEventListener("mousemove", function(e) {
        if (!rightclickopen) {
            document.getElementById("rightclick_menu").style.transform = "translate(" + (e.clientX - 7) + "px," + (e.clientY- 780) + "px)";
        }
    })

    canvas.addEventListener("mousemove", function(e) {
        var rect = canvas.getBoundingClientRect();
        mousex = e.clientX - rect.left,
        mousey = e.clientY - rect.top
    })

    canvas.addEventListener("mousedown", function(e) {
        if (e.button == 0) {
            mousedown = true

            objectselect = null;
            for (let index = 0; index < objects.length; index++) {
                const object = objects[index];

                if (withinBounds(object.x, object.y, object.w, object.h, mousex, mousey)) {
                    select(index);
                    break
                }
            }
        }
    })

    canvas.addEventListener("mouseup", function(e) {
        if (e.button == 0) {
            mousedown = false
        }
    })

    document.getElementById("prop_image_tex").addEventListener("keyup", function(e) {
        if (objectselect != null) {
            for (let index = 0; index < objects.length; index++) {
                const object = objects[index];
                
                console.log(document.getElementById("prop_image_tex").value);
                console.log(object.type);

                if (object.name == document.getElementById("prop_image_tex").value && object.type == "texture") {
                    objectselect.tex = object;
                    break
                }
            }
        }
    })

    document.getElementById("prop_name").addEventListener("keyup", function(e) {
        if (objectselect != null) {
            document.getElementById(objectselect.id).innerHTML = document.getElementById("prop_name").value;
            objectselect.name = document.getElementById("prop_name").value;
        }
    })

    document.getElementById("prop_x").addEventListener("keyup", function(e) {
        if (objectselect != null) {
            objectselect.x = document.getElementById("prop_x").value;
        }
    })

    document.getElementById("prop_y").addEventListener("keyup", function(e) {
        if (objectselect != null) {
            objectselect.y = document.getElementById("prop_y").value;
        }
    })

    document.getElementById("prop_w").addEventListener("keyup", function(e) {
        if (objectselect != null) {
            objectselect.w = document.getElementById("prop_w").value;
        }
    })

    document.getElementById("prop_h").addEventListener("keyup", function(e) {
        if (objectselect != null) {
            objectselect.h = document.getElementById("prop_h").value;
        }
    })

    document.getElementById("prop_texture_path").addEventListener("keyup", function(e) {
        if (objectselect != null) {
            objectselect.path = document.getElementById("prop_texture_path").value;
        }
    })

    canvas.addEventListener("contextmenu", event => event.preventDefault());

    renderLoop();
}

var fpsTarget = 60

var delta = 0;
var deltalast = 0;

function dataLoop() {
    if (objectselect != null && mousedown) {
        objectselect.x = mousex - objectselect.w / 2
        objectselect.y = mousey - objectselect.h / 2

        document.getElementById("prop_x").value = objectselect.x;
        document.getElementById("prop_y").value = objectselect.y;
    }
}

function renderLoop() {
    var date = new Date();
    
    dataLoop()
    
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    if (objectselect != null) {
        ctx.strokeStyle = "#0023FF";
        ctx.strokeRect(objectselect.x, objectselect.y, objectselect.w, objectselect.h);
    }

    for (let index = 0; index < objects.length; index++) {
        const object = objects[index];
        
        if (object.type == "image") {
            if (object.tex != null && object.tex.img != null) {
                ctx.drawImage(object.tex.img, object.x, object.y, object.w, object.h);
            }
        }
    }

    delta = date.getTime() / 1000 - deltalast;
    deltalast = date.getTime() / 1000;

    document.getElementById("stats").innerHTML = "FPS: " + Math.round(1 / delta)

    document.getElementById("code").textContent = generateCode();

    setTimeout(renderLoop, (1 / fpsTarget * 1000))
}