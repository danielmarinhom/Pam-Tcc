package main

import (
	"github.com/gin-gonic/gin"
	"net/http"
	"github.com/google/uuid"
	"sync"
)

type Usuario struct {
	ID       string `json:"id,omitempty"`
	Telefone string `json:"telefone" binding:"required"`
	Senha    string `json:"senha" binding:"required"`
}

//db em memoria (pra nao ter que guardar em sql)
var usuariosDB = struct{
	sync.RWMutex
	data map[string]Usuario
}{data: make(map[string]Usuario)}

func main() {
	r := gin.Default()

	r.POST("/usuario/Registrar", func(c *gin.Context) {
		var novoUsuario Usuario
		if err := c.ShouldBindJSON(&novoUsuario); err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		usuariosDB.RLock()
		for _, u := range usuariosDB.data {
			if u.Telefone == novoUsuario.Telefone {
				usuariosDB.RUnlock()
				c.JSON(http.StatusBadRequest, gin.H{"error": "telefone ja existe"})
				return
			}
		}
		usuariosDB.RUnlock()

		novoUsuario.ID = uuid.NewString()
		usuariosDB.Lock()
		usuariosDB.data[novoUsuario.ID] = novoUsuario
		usuariosDB.Unlock()

		c.JSON(http.StatusOK, novoUsuario)
	})

	//r.POST("/usuario/Autenticar", func(c *gin.Context) {
	//	var login Usuario
	//	if err := c.ShouldBindJSON(&login); err != nil {
	//		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
	//		return
	//	}
//
	//	usuariosDB.RLock()
	//	defer usuariosDB.RUnlock()
	//	for _, u := range usuariosDB.data {
	//		if u.Telefone == login.Telefone && u.Senha == login.Senha {
	//			c.JSON(http.StatusOK, u)
	//			return
	//		}
	//	}
//
	//	c.JSON(http.StatusUnauthorized, gin.H{"error": "tel"})
	//})

	r.GET("/usuario/Listar", func(c *gin.Context) {
		usuariosDB.RLock()
		defer usuariosDB.RUnlock()
		c.JSON(http.StatusOK, usuariosDB.data)
	})

	r.Run(":8080"
}
