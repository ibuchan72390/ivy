namespace Ivy.MailChimp.Core.Models
{
    /*
     * Seems that MailChimp is expecting these values in all caps
     * If they're not laid out in capital letters, the addition
     * will fail to save the name appropriately.
     */

    public class MailChimpMergeFields
    {
        public string NAME { get; set; }

        public string ROLE { get; set; }

        public string ADDRESS { get; set; }

        public string PHONE { get; set; }

        public string MORE { get; set; }
    }
}
