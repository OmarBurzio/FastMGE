using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juppiter.DL
{
    public enum ItemEventoStato
    {
        OK,
        Warning,
        Error
    }

    public class ItemEvento
    {
        private ItemEventoStato statoInternal;

        public ItemEventoStato Stato {
            get {
                return statoInternal;
            }
        }
        private string descrizioneInternal;

        public string Descrizione {
            get {
                return descrizioneInternal;
            }
        }
        
        public ItemEvento()
        {
            this.statoInternal = ItemEventoStato.OK;
            this.descrizioneInternal = String.Empty;
        } 

        private bool setStato(ItemEventoStato stato)
        {
            if(this.statoInternal <= stato)
            {
                statoInternal = stato;
                return true;
            }
            return false;
        }

        public void AddMessage(string Descrizione)
        {
            if (setStato(ItemEventoStato.OK))
            {
                this.descrizioneInternal = Descrizione;
            }
        }

        public void AddWarning(string Descrizione)
        {
            if (setStato(ItemEventoStato.Warning))
            {
                this.descrizioneInternal = Descrizione;
            }
        }

        public void AddError(Exception ex)
        {
            setStato(ItemEventoStato.Error);
            this.descrizioneInternal = ex.Message;
        }
    }
}
