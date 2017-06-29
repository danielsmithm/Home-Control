using HomeControl.Domain.Residencia;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeControl.Domain.Dispositivos
{
    public class Embarcado : IActivable, IPersistable<int>
    {
        [Key]
        private int id;
        private String nome;
        private String socket;
        private String macAddress;
        private HashSet<Dispositivo> dispositivos;

       
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public String Nome
        {
            get
            {
                return this.nome;
            }
            set
            {
                this.nome = value;
            }
        }

        public String Socket
        {
            get
            {
                return this.socket;
            }
            set
            {
                this.socket = value;
            }
        }

        public String MacAddress
        {
            get
            {
                return this.macAddress;
            }
            set
            {
                this.macAddress = value;
            }
        }

        /*public int ComodoId
        {
            get
            {
                return this.comodoId;
            }
            set
            {
                this.comodoId = value;
            }
        }*/

        public HashSet<Dispositivo> Dispositivos
        {
            get
            {
                return this.dispositivos;
            }
            set
            {
                this.dispositivos = value;
            }
        }

        void validarDispositivos()
        {
            throw new NotImplementedException();
        }

        public void Activate()
        {
            throw new NotImplementedException();
        }

        public void Deactivate()
        {
            throw new NotImplementedException();
        }

        public bool IsActive()
        {
            throw new NotImplementedException();
        }

        public int ComodoId { get; set; }
        [ForeignKey("ComodoId")]
        public virtual Comodo Comodo
        {
            get; protected set;
        }
    }
}