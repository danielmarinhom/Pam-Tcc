from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
import uuid
import threading

app = FastAPI()

class Usuario(BaseModel):
    id: str | None = None
    telefone: str
    senha: str

# "DB" em memória
usuarios_db = {}
lock = threading.RLock()

@app.post("/usuario/Registrar")
def registrar_usuario(novo_usuario: Usuario):
    with lock:
        # Verificar se já existe telefone
        for u in usuarios_db.values():
            if u.telefone == novo_usuario.telefone:
                raise HTTPException(status_code=400, detail="telefone já existe")

        novo_usuario.id = str(uuid.uuid4())
        usuarios_db[novo_usuario.id] = novo_usuario
        return novo_usuario

# Autenticar (equivalente ao comentado no Go)
@app.post("/usuario/Autenticar")
def autenticar_usuario(login: Usuario):
    with lock:
        for u in usuarios_db.values():
            if u.telefone == login.telefone and u.senha == login.senha:
                return u
    raise HTTPException(status_code=401, detail="telefone ou senha incorretos")

@app.get("/usuario/Listar")
def listar_usuarios():
    with lock:
        return usuarios_db
